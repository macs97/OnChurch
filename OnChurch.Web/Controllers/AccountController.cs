using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnChurch.Common.Enum;
using OnChurch.Common.Responses;
using OnChurch.Web.Data;
using OnChurch.Web.Data.Entities;
using OnChurch.Web.Helpers;
using OnChurch.Web.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnChurch.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataContext _context;
        private readonly IBlobHelper _blobHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IUserHelper _userHelper;
        private readonly IMailHelper _mailHelper;

        public AccountController(DataContext context, IBlobHelper blobHelper, IConverterHelper converterHelper, IUserHelper userHelper, ICombosHelper combosHelper,
            IMailHelper mailHelper)
        {
            _context = context;
            _blobHelper = blobHelper;
            _converterHelper = converterHelper;
            _combosHelper = combosHelper;
            _userHelper = userHelper;
            _mailHelper = mailHelper;
        }

        public IActionResult Register()
        {
            AddMemberViewModel model = new AddMemberViewModel
            {
                Professions = _combosHelper.GetComboProfessions(),
                Campuses = _combosHelper.GetComboCampus(),
                Sections = _combosHelper.GetComboSection(0),
                Churches = _combosHelper.GetComboChurch(0)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AddMemberViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;

                if (model.PhotoId != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.PhotoFile, "members");
                }

                try
                {
                    User member = await _userHelper.AddMemberAsync(model, imageId, UserType.Member);
                    if (member == null)
                    {
                        ModelState.AddModelError(string.Empty, "This email is already used.");
                        model.Campuses = _combosHelper.GetComboCampus();
                        model.Sections = _combosHelper.GetComboSection(model.CampusId);
                        model.Churches = _combosHelper.GetComboChurch(model.ChurchId);
                        return View(model);
                    }

                    string myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(member);
                    string tokenLink = Url.Action("ConfirmEmail", "Account", new
                    {
                        memberId = member.Id,
                        token = myToken
                    }, protocol: HttpContext.Request.Scheme);

                    Response response = _mailHelper.SendMail(model.Username, "Email confirmation", $"<h1>Email Confirmation</h1>" +
                        $"To allow the user, " +
                        $"plase click in this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");
                    if (response.IsSuccess)
                    {
                        ViewBag.Message = "The instructions to allow your user has been sent to email.";
                        return View(model);
                    }

                    ModelState.AddModelError(string.Empty, response.Message);


                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            model.Professions = _combosHelper.GetComboProfessions();
            model.Campuses = _combosHelper.GetComboCampus();
            model.Sections = _combosHelper.GetComboSection(model.CampusId);
            model.Churches = _combosHelper.GetComboChurch(model.ChurchId);
            return View(model);
        }

        public async Task<IActionResult> ChangeMember()
        {
            User member = await _userHelper.GetMemberAsync(User.Identity.Name);

            if (member == null)
            {
                return NotFound();
            }

            EditMemberViewModel model = await _converterHelper.ToMemberViewModelAsync(member);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeMember(EditMemberViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Guid imageId = model.PhotoId;

                    if (model.PhotoFile != null)
                    {
                        imageId = await _blobHelper.UploadBlobAsync(model.PhotoFile, "members");
                    }
                    User member = await _userHelper.GetMemberAsync(User.Identity.Name);

                    member.FirstName = model.FirstName;
                    member.LastName = model.LastName;
                    member.Address = model.Address;
                    member.PhoneNumber = model.PhoneNumber;
                    member.PhotoId = imageId;
                    member.Church = await _context.Churches.FindAsync(model.ChurchId);
                    member.Document = model.Document;


                    await _userHelper.UpdateMemberAsync(member);
                    return RedirectToAction("Index", "Home");

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            model.Professions = _combosHelper.GetComboProfessions();
            return View(model);
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Email or password incorrect.");
            }

            return View(model);
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userHelper.GetMemberAsync(User.Identity.Name);
                if (user != null)
                {
                    IdentityResult result = await _userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("ChangeMember");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Member no found.");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string memberId, string token)
        {
            if (string.IsNullOrEmpty(memberId) || string.IsNullOrEmpty(token))
            {
                return NotFound();
            }

            User member = await _userHelper.GetMemberAsync(new Guid(memberId));
            if (member == null)
            {
                return NotFound();
            }

            IdentityResult result = await _userHelper.ConfirmEmailAsync(member, token);
            if (!result.Succeeded)
            {
                return NotFound();
            }

            return View();
        }


        public IActionResult NotAuthorized()
        {
            return View();
        }


        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<JsonResult> GetSectionsAsync(int campusId)
        {

            Campus campus = await _context.Campuses.Include(c => c.Sections).FirstOrDefaultAsync(c => c.Id == campusId);
            return Json(campus.Sections.OrderBy(s => s.Name));
        }

        public async Task<JsonResult> GetChurchsAsync(int sectionId)
        {
            Section section = await _context.Sections
                .Include(s => s.Churches)
                .FirstOrDefaultAsync(s => s.Id == sectionId);
            return Json(section.Churches.OrderBy(s => s.Name));
        }

        public IActionResult RecoverPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User member = await _userHelper.GetMemberAsync(model.Email);
                if (member == null)
                {
                    ModelState.AddModelError(string.Empty, "The email doesn't correspont to a registered user.");
                    return View(model);
                }

                string myToken = await _userHelper.GeneratePasswordResetTokenAsync(member);
                string link = Url.Action(
                    "ResetPassword",
                    "Account",
                    new { token = myToken }, protocol: HttpContext.Request.Scheme);
                _mailHelper.SendMail(model.Email, "Password Reset", $"<h1>Password Reset</h1>" +
                    $"To reset the password click in this link:</br></br>" +
                    $"<a href = \"{link}\">Reset Password</a>");
                ViewBag.Message = "The instructions to recover your password has been sent to email.";
                return View();

            }

            return View(model);
        }

        public IActionResult ResetPassword(string token)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            User member = await _userHelper.GetMemberAsync(model.UserName);
            if (member != null)
            {
                IdentityResult result = await _userHelper.ResetPasswordAsync(member, model.Token, model.Password);
                if (result.Succeeded)
                {
                    ViewBag.Message = "Password reset successful.";
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.Message = "Error while resetting the password.";
                return View(model);
            }

            ViewBag.Message = "User not found.";
            return View(model);
        }

    }

}
