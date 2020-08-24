using OnChurch.Common.Entities;
using OnChurch.Web.Models;
using System;
using System.Threading.Tasks;

namespace OnChurch.Web.Helpers
{
    public interface IConverterHelper
    {
        Task<Member> ToMemberAsync(MemberViewModel model, Guid photoId, bool isNew);
        MemberViewModel toMemberViewModel(Member member);

    }
}
