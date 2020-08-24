using OnChurch.Common.Entities;
using OnChurch.Web.Data;
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
        public async Task<Member> ToMemberAsync(MemberViewModel model, Guid photoId, bool isNew)
        {
            return new Member
            {
                Id = isNew ? 0 : model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                Document = model.Document,
                Email = model.Email,
                Phone = model.Phone,
                PhotoId = photoId,
                Profession = await _context.Professions.FindAsync(model.ProfessionId)
            };
        }

        public MemberViewModel toMemberViewModel(Member member)
        {
            return new MemberViewModel
            {
                Id = member.Id,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Address = member.Address,
                Document = member.Document,
                Email = member.Email,
                Phone = member.Phone,
                PhotoId = member.PhotoId,
                Profession = member.Profession,
                ProfessionId = member.IdProfession,
                Professions = _combosHelper.GetComboProfessions()
            };
        }

    }
}
