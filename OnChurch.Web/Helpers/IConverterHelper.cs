using OnChurch.Web.Data.Entities;
using OnChurch.Web.Models;
using System;
using System.Threading.Tasks;

namespace OnChurch.Web.Helpers
{
    public interface IConverterHelper
    {
        Task<User> ToMemberAsync(EditMemberViewModel model, Guid photoId, bool isNew);
        Task<EditMemberViewModel> ToMemberViewModelAsync(User member);

    }
}
