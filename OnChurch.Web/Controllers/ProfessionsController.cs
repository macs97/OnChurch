﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnChurch.Common.Entities;
using OnChurch.Web.Data;

namespace OnChurch.Web.Controllers
{
    public class ProfessionsController : Controller
    {
        private readonly DataContext _context;

        public ProfessionsController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Professions.ToListAsync());
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Profession profession)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(profession);
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
            return View(profession);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profession = await _context.Professions.FindAsync(id);
            if (profession == null)
            {
                return NotFound();
            }
            return View(profession);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Profession profession)
        {
            if (id != profession.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profession);
                    await _context.SaveChangesAsync();
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
            return View(profession);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .Include(m => m.Profession)
                .FirstOrDefaultAsync(m => m.Profession.Id == id);

            if(member == null)
            {
                var profession = await _context.Professions
                .FirstOrDefaultAsync(p => p.Id == id);
                if (profession == null)
                {
                    return NotFound();
                }

                try
                {
                    _context.Professions.Remove(profession);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            else 
            {
                // TODO
                ModelState.AddModelError(string.Empty, "This record have to one or more members asociated");
            }
                
            

            return RedirectToAction(nameof(Index));
        }
    }
}
