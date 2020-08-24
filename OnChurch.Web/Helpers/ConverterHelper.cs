using OnChurch.Common.Entities;
using OnChurch.Web.Models;
using System;

namespace OnChurch.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public Member ToMember(MemberViewModel model, Guid photoId, bool isNew)
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
                Profession = model.Profession
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
                Profession = member.Profession
            };
        }
    }
}
