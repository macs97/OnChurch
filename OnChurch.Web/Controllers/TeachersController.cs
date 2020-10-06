using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnChurch.Web.Data;
using OnChurch.Web.Data.Entities;
using OnChurch.Web.Helpers;
using OnChurch.Web.Models;

namespace OnChurch.Web.Controllers
{
    public class TeachersController : Controller
    {
        private readonly DataContext _context;
        private readonly IBlobHelper _blobHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IUserHelper _userHelper;
        private readonly IMailHelper _mailHelper;

        public TeachersController(DataContext context, IBlobHelper blobHelper, IConverterHelper converterHelper, IUserHelper userHelper, ICombosHelper combosHelper,
            IMailHelper mailHelper)
        {
            _context = context;
            _blobHelper = blobHelper;
            _converterHelper = converterHelper;
            _combosHelper = combosHelper;
            _userHelper = userHelper;
            _mailHelper = mailHelper;
        }

        public async Task<IActionResult> Members()
        {
            User user = await _userHelper.GetMemberAsync(User.Identity.Name);
            List<User> users = await _context.Users.Include(u => u.Church).Where(c => c.Church == user.Church && c.UserType == Common.Enum.UserType.Member).ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> IndexMeeting()
        {
            User user = await _userHelper.GetMemberAsync(User.Identity.Name);
            return View(_context.Meetings.Include(m => m.Church).Where(c => c.Church == user.Church));
        }

        public async Task<IActionResult> Meeting()
        {
            User user = await _userHelper.GetMemberAsync(User.Identity.Name);
            AddMeetingViewModel model = new AddMeetingViewModel
            {
                ChurchId = user.Church.Id,
                Assistances = new List<Assistance>()
            };
            return View(model);
        }

        public async Task<IActionResult> Assistance(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            User user = await _userHelper.GetMemberAsync(User.Identity.Name);
            List<User> users = await _context.Users.Where(u => u.Church.Id == user.Church.Id ).ToListAsync();
            Meeting meeting = await _context.Meetings.Include(m => m.Assistances).FirstOrDefaultAsync(m => m.Id == id);
            
            return View(meeting.Assistances);
        }

        [HttpPost]
        public async Task<IActionResult> Meeting(AddMeetingViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Meeting meeting = new Meeting
                    {
                        Date = model.Date,
                        Church = _context.Churches.FirstOrDefault(c => c.Id == model.ChurchId),
                        Assistances = model.Assistances
                    };
                    User user = await _userHelper.GetMemberAsync(User.Identity.Name);
                    List<User> users = await _context.Users.Where(u => u.Church.Id == user.Church.Id).ToListAsync();
                    users.ForEach(user =>
                    {
                        meeting.Assistances.Add(new Assistance
                        {
                            Meeting = meeting,
                            User = user
                        });
                    });
                    _context.Add(meeting);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(IndexMeeting));
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
            return View(model);
        }

        public async Task<IActionResult> DetailsMembers(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User member = await _context.Users
                .Include(m => m.Profession)
                .FirstOrDefaultAsync(m => m.Id == id);
            member.Id = id;
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }
    }
}
