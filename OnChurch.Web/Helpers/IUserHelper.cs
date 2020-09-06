using Microsoft.AspNetCore.Identity;
using OnChurch.Common.Entities;
using OnChurch.Common.Enum;
using OnChurch.Web.Data.Entities;
using OnChurch.Web.Models;
using System;
using System.Threading.Tasks;

namespace OnChurch.Web.Helpers
{
    public interface IUserHelper
    {
        Task<Member> GetMemberAsync(string email);

        Task<IdentityResult> AddMemberAsync(Member member, string password);

        Task CheckRoleAsync(string roleName);

        Task AddMemberToRoleAsync(Member member, string roleName);

        Task<bool> IsMemberInRoleAsync(Member member, string roleName);

        Task<Member> AddMemberAsync(AddMemberViewModel model, Guid photoId, UserType userType);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

        Task<SignInResult> ValidatePasswordAsync(Member member, string password);

        Task<IdentityResult> UpdateMemberAsync(Member member);

        Task<Member> GetMemberAsync(Guid memberId);

        Task<IdentityResult> ChangePasswordAsync(Member member, string oldPassword, string newPassword);

        Task<string> GenerateEmailConfirmationTokenAsync(Member member);

        Task<IdentityResult> ConfirmEmailAsync(Member member, string token);

        Task<string> GeneratePasswordResetTokenAsync(Member member);

        Task<IdentityResult> ResetPasswordAsync(Member member, string token, string password);

        Task<Church> GetChurchAsync(int idChurch);

    }

}
