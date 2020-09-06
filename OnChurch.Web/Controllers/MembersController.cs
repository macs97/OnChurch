using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnChurch.Common.Entities;
using OnChurch.Common.Enum;
using OnChurch.Web.Data;
using OnChurch.Web.Data.Entities;
using OnChurch.Web.Helpers;
using OnChurch.Web.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnChurch.Web.Controllers
{
    public class MembersController : Controller
    {
        private readonly DataContext _context;
        private readonly IBlobHelper _blobHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IUserHelper _userHelper;

        public MembersController(DataContext context, IBlobHelper blobHelper, IConverterHelper converterHelper, IUserHelper userHelper, ICombosHelper combosHelper)
        {
            _context = context;
            _blobHelper = blobHelper;
            _converterHelper = converterHelper;
            _combosHelper = combosHelper;
            _userHelper = userHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Users
                .Include(m => m.Profession)
                .ToListAsync());
        }

        public IActionResult Create()
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
        public async Task<IActionResult> Create(AddMemberViewModel model)
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
                    Member member = await _userHelper.AddMemberAsync(model, imageId, UserType.User);
                    //_context.Add(member);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
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

        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Member member = await _context.Users
                .Include(m => m.Profession)
                .FirstOrDefaultAsync(m => m.Id == id);
            member.Id = id;
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Member member = await _context.Users
                .Include(m => m.Profession)
                .Include(m => m.Church)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (member == null)
            {
                return NotFound();
            }



            EditMemberViewModel model = await _converterHelper.ToMemberViewModelAsync(member);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditMemberViewModel model)
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
                    Member member = await _converterHelper.ToMemberAsync(model, imageId, false);
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

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

        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Member member = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            try
            {
                _context.Users.Remove(member);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return RedirectToAction(nameof(Index));
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
    }
}
