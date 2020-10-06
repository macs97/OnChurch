using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnChurch.Web.Data;
using OnChurch.Web.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnChurch.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CampusesController : Controller
    {
        private readonly DataContext _context;

        public CampusesController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Campuses
                .Include(c => c.Sections)
                .ToListAsync());
        }

        // GET: Campuses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Campus campus = await _context.Campuses
                .Include(c => c.Sections)
                .ThenInclude(ch => ch.Churches)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (campus == null)
            {
                return NotFound();
            }

            return View(campus);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Campus campus)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(campus);
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
            return View(campus);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Campus campus = await _context.Campuses.FindAsync(id);
            if (campus == null)
            {
                return NotFound();
            }
            return View(campus);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Campus campus)
        {
            if (id != campus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(campus);
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
            return View(campus);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Campus campus = await _context.Campuses
                .Include(s => s.Sections)
                .ThenInclude(c => c.Churches)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (campus == null)
            {
                return NotFound();
            }

            _context.Campuses.Remove(campus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> AddSection(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Campus campus = await _context.Campuses.FindAsync(id);
            if (campus == null)
            {
                return NotFound();
            }

            Section model = new Section { CampusId = campus.Id };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSection(Section section)
        {
            if (ModelState.IsValid)
            {
                Campus campus = await _context.Campuses
                    .Include(c => c.Sections)
                    .FirstOrDefaultAsync(c => c.Id == section.CampusId);
                if (campus == null)
                {
                    return NotFound();
                }

                try
                {
                    section.Id = 0;
                    campus.Sections.Add(section);
                    _context.Update(campus);
                    await _context.SaveChangesAsync();
                    return RedirectToAction($"{nameof(Details)}/{campus.Id}");
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

            return View(section);
        }
        public async Task<IActionResult> EditSection(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Section section = await _context.Sections.FindAsync(id);
            if (section == null)
            {
                return NotFound();
            }

            Campus campus = await _context.Campuses.FirstOrDefaultAsync(c => c.Sections.FirstOrDefault(d => d.Id == section.Id) != null);
            section.CampusId = campus.Id;
            return View(section);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSection(Section section)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(section);
                    await _context.SaveChangesAsync();
                    return RedirectToAction($"{nameof(Details)}/{section.CampusId}");

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
            return View(section);
        }

        public async Task<IActionResult> DeleteSection(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Section section = await _context.Sections
                .Include(d => d.Churches)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (section == null)
            {
                return NotFound();
            }

            Campus campus = await _context.Campuses.FirstOrDefaultAsync(c => c.Sections.FirstOrDefault(d => d.Id == section.Id) != null);
            _context.Sections.Remove(section);
            await _context.SaveChangesAsync();
            return RedirectToAction($"{nameof(Details)}/{campus.Id}");
        }

        public async Task<IActionResult> DetailsSection(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Section section = await _context.Sections
                .Include(s => s.Churches)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (section == null)
            {
                return NotFound();
            }

            Campus campus = await _context.Campuses.FirstOrDefaultAsync(c => c.Sections.FirstOrDefault(d => d.Id == section.Id) != null);
            section.CampusId = campus.Id;
            return View(section);
        }

        public async Task<IActionResult> AddChurch(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Section section = await _context.Sections.FindAsync(id);
            if (section == null)
            {
                return NotFound();
            }

            Church model = new Church { IdSection = section.Id };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddChurch(Church church)
        {
            if (ModelState.IsValid)
            {
                Section section = await _context.Sections
                    .Include(d => d.Churches)
                    .FirstOrDefaultAsync(c => c.Id == church.IdSection);
                if (section == null)
                {
                    return NotFound();
                }

                try
                {
                    church.Id = 0;
                    section.Churches.Add(church);
                    _context.Update(section);
                    await _context.SaveChangesAsync();
                    return RedirectToAction($"{nameof(DetailsSection)}/{section.Id}");

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

            return View(church);
        }

        public async Task<IActionResult> EditChurch(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Church church = await _context.Churches.FindAsync(id);
            if (church == null)
            {
                return NotFound();
            }

            Section section = await _context.Sections.FirstOrDefaultAsync(d => d.Churches.FirstOrDefault(c => c.Id == church.Id) != null);
            church.IdSection = section.Id;
            return View(church);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditChurch(Church church)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(church);
                    await _context.SaveChangesAsync();
                    return RedirectToAction($"{nameof(DetailsSection)}/{church.IdSection}");

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
            return View(church);
        }

        public async Task<IActionResult> DeleteChurch(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Church church = await _context.Churches
                .FirstOrDefaultAsync(m => m.Id == id);
            if (church == null)
            {
                return NotFound();
            }

            Section section = await _context.Sections.FirstOrDefaultAsync(d => d.Churches.FirstOrDefault(c => c.Id == church.Id) != null);
            _context.Churches.Remove(church);
            await _context.SaveChangesAsync();
            return RedirectToAction($"{nameof(DetailsSection)}/{section.Id}");
        }

    }

}
