using OnChurch.Web.Data;
using OnChurch.Web.Data.Entities;
using OnChurch.Web.Models;
using System;
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

        public EditMemberViewModel toMemberViewModel(Member member)
        {
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
                Profession = member.Profession,
                ProfessionId = member.IdProfession,
                Professions = _combosHelper.GetComboProfessions(),
                Church = member.Church,
                Churches = _combosHelper.GetComboChurch(member.Church.IdSection),
                ChurchId = member.Church.Id,
            };
        }

    }
}
