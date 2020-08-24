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

        public MembersController(DataContext context, IBlobHelper blobHelper, IConverterHelper converterHelper)
        {
            _context = context;
            _blobHelper = blobHelper;
            _converterHelper = converterHelper;

        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Members.ToListAsync());
        }

        public IActionResult Create()
        {
            MemberViewModel model = new MemberViewModel();
            return View(model);
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
                    Member member = _converterHelper.ToMember(model, imageId, true);
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

            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Member member = await _context.Members.FindAsync(id);
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
                Guid imageId = model.PhotoId;

                if (model.PhotoId != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.PhotoFile, "members");
                }

                try
                {
                    Member member = _converterHelper.ToMember(model, imageId, false);
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
