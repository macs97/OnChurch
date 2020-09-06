using Microsoft.EntityFrameworkCore;
using OnChurch.Common.Entities;
using OnChurch.Web.Data;
using OnChurch.Web.Data.Entities;
using OnChurch.Web.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnChurch.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        public ConverterHelper(DataContext context, ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }
        public async Task<Member> ToMemberAsync(EditMemberViewModel model, Guid photoId, bool isNew)
        {
            return new Member
            {
                Id = isNew ? "" : model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                Document = model.Document,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                PhotoId = photoId,
                Profession = await _context.Professions.FindAsync(model.ProfessionId)
            };
        }

        public async Task<EditMemberViewModel> ToMemberViewModelAsync(Member member)
        {
            Section section = await _context.Sections.FirstOrDefaultAsync(s => s.Churches.FirstOrDefault(c => c.Id == member.Church.Id) != null);
            Campus campus = await _context.Campuses.FirstOrDefaultAsync(c => c.Sections.FirstOrDefault(s => s.Id == section.Id) != null);
            Profession profession = await _context.Professions.FirstOrDefaultAsync(p => p.Id == member.Profession.Id);

            return new EditMemberViewModel
            {
                Id = member.Id,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Address = member.Address,
                Document = member.Document,
                Email = member.Email,
                PhoneNumber = member.PhoneNumber,
                PhotoId = member.PhotoId,
                Profession = profession,
                ProfessionId = profession.Id,
                Professions = _combosHelper.GetComboProfessions(),
                Church = member.Church,
                Churches = _combosHelper.GetComboChurch(section.Id),
                ChurchId = member.Church.Id,
                CampusId = campus.Id,
                SectionId = section.Id,
                Campuses = _combosHelper.GetComboCampus(),
                Sections = _combosHelper.GetComboSection(campus.Id)
            };
        }

    }
}
