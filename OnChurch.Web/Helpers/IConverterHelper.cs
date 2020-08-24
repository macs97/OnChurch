using OnChurch.Common.Entities;
using OnChurch.Web.Models;
using System;

namespace OnChurch.Web.Helpers
{
    public interface IConverterHelper
    {
        Member ToMember(MemberViewModel model, Guid photoId, bool isNew);
        MemberViewModel toMemberViewModel(Member member);
    }
}
