using Microsoft.AspNetCore.Identity;
using OnChurch.Common.Enum;
using OnChurch.Web.Data.Entities;
using OnChurch.Web.Models;
using System;
using System.Threading.Tasks;

namespace OnChurch.Web.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetMemberAsync(string email);

        Task<IdentityResult> AddMemberAsync(User member, string password);

        Task CheckRoleAsync(string roleName);

        Task AddMemberToRoleAsync(User member, string roleName);

        Task<bool> IsMemberInRoleAsync(User member, string roleName);

        Task<User> AddMemberAsync(AddMemberViewModel model, Guid photoId, UserType userType);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

        Task<SignInResult> ValidatePasswordAsync(User member, string password);

        Task<IdentityResult> UpdateMemberAsync(User member);

        Task<User> GetMemberAsync(Guid memberId);

        Task<IdentityResult> ChangePasswordAsync(User member, string oldPassword, string newPassword);

        Task<string> GenerateEmailConfirmationTokenAsync(User member);

        Task<IdentityResult> ConfirmEmailAsync(User member, string token);

        Task<string> GeneratePasswordResetTokenAsync(User member);

        Task<IdentityResult> ResetPasswordAsync(User member, string token, string password);

        Task<Church> GetChurchAsync(int idChurch);

    }

}
