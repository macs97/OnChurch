using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnChurch.Common.Entities;
using OnChurch.Web.Data;
using OnChurch.Web.Helpers;
using OnChurch.Web.Models;
using System;
using System.Threading.Tasks;

namespace OnChurch.Web.Controllers
{
    public class MembersController : Controller
    {
        private readonly DataContext _context;
        private readonly IBlobHelper _blobHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly ICombosHelper _combosHelper;

        public MembersController(DataContext context, IBlobHelper blobHelper, IConverterHelper converterHelper, ICombosHelper combosHelper)
        {
            _context = context;
            _blobHelper = blobHelper;
            _converterHelper = converterHelper;
            _combosHelper = combosHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Members
                .Include(m => m.Profession)
                .ToListAsync());
        }

        public IActionResult Create()
        {
            MemberViewModel model = new MemberViewModel
            {
                Professions = _combosHelper.GetComboProfessions()
            };
            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Member member = await _context.Members
                .Include(m => m.Profession)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MemberViewModel model)
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
                    Member member = await _converterHelper.ToMemberAsync(model, imageId, true);
                    _context.Add(member);
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

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Member member = await _context.Members
                .Include(m => m.Profession)
                .FirstOrDefaultAsync(m => m.Id == id);
            member.IdProfession = member.Profession.Id;
            if (member == null)
            {
                return NotFound();
            }

            MemberViewModel model = _converterHelper.toMemberViewModel(member);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MemberViewModel model)
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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Member member = await _context.Members
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            try
            {
                _context.Members.Remove(member);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
