using OnChurch.Common.Entities;
using OnChurch.Web.Data.Entities;
using OnChurch.Web.Models;
using System;
using System.Threading.Tasks;

namespace OnChurch.Web.Helpers
{
    public interface IConverterHelper
    {
        Task<Member> ToMemberAsync(EditMemberViewModel model, Guid photoId, bool isNew);
        EditMemberViewModel toMemberViewModel(Member member);

    }
}
