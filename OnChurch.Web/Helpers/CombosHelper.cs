using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnChurch.Common.Entities;
using OnChurch.Web.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnChurch.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetComboProfessions()
        {
            List<SelectListItem> list = _context.Professions.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = $"{t.Id}"
            })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a profession...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboCampus()
        {
            List<SelectListItem> list = _context.Campuses.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = $"{t.Id}"
            })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a campus...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboSection(int campusId)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            Campus campus = _context.Campuses.Include(c => c.Sections).FirstOrDefault(c => c.Id == campusId);
            if (campus != null)
            {
                list = campus.Sections.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = $"{c.Id}"
                }).OrderBy(t => t.Text).ToList();
            }
            list.Insert(0, new SelectListItem
            {
                Text = "[Select a section...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboChurch(int sectionId)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            Section section = _context.Sections.Include(c => c.Churches).FirstOrDefault(s => s.Id == sectionId);
            if (section != null)
            {
                list = section.Churches.Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = $"{t.Id}"
                })
                .OrderBy(t => t.Text)
                .ToList();
            }

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a church...]",
                Value = "0"
            });

            return list;
        }

        /*public async Task<Campus> GetCampusAsync(Section section)
        {
            return await _context.Campuses.Where(c => c.Sections.Any(s => s.Id == section.Id)).FirstOrDefaultAsync();
        }

        public async Task<Section> GetSectionAsync(Church church)
        {
            return await _context.Sections.Where(c => c.Churches.Any(ch => ch.Id == church.Id)).FirstOrDefaultAsync();
        }

        public async Task<Church> GetChurchAsync(int churchId)
        {
            return await _context.Churches.Where(c => c.Id == churchId).FirstOrDefaultAsync();
        }*/
    }

}
