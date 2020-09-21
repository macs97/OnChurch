using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnChurch.Common.Enum;
using OnChurch.Web.Data;
using OnChurch.Web.Data.Entities;
using OnChurch.Web.Models;
using System;
using System.Threading.Tasks;

namespace OnChurch.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public UserHelper(DataContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> AddMemberAsync(User member, string password)
        {
            return await _userManager.CreateAsync(member, password);
        }

        public async Task AddMemberToRoleAsync(User member, string roleName)
        {
            await _userManager.AddToRoleAsync(member, roleName);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            bool roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }
        }

        public async Task<User> GetMemberAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Church)
                .Include(u => u.Profession)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> IsMemberInRoleAsync(User member, string roleName)
        {
            return await _userManager.IsInRoleAsync(member, roleName);
        }

        public async Task<User> AddMemberAsync(AddMemberViewModel model, Guid photoId, UserType userType)
        {

            User member = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                Document = model.Document,
                Email = model.Username,
                PhoneNumber = model.PhoneNumber,
                PhotoId = photoId,
                Profession = await _context.Professions.FindAsync(model.ProfessionId),
                Church = await _context.Churches.FindAsync(model.ChurchId),
                UserName = model.Username,
                UserType = userType
            };

            IdentityResult result = await _userManager.CreateAsync(member, model.Password);
            if (result != IdentityResult.Success)
            {
                return null;
            }

            User newMember = await GetMemberAsync(model.Username);
            await AddMemberToRoleAsync(newMember, member.UserType.ToString());
            return newMember;
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(
                model.Username,
                model.Password,
                model.RememberMe,
                false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<SignInResult> ValidatePasswordAsync(User member, string password)
        {
            return await _signInManager.CheckPasswordSignInAsync(member, password, false);
        }

        public async Task<IdentityResult> UpdateMemberAsync(User member)
        {
            return await _userManager.UpdateAsync(member);
        }

        public async Task<IdentityResult> ChangePasswordAsync(User member, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(member, oldPassword, newPassword);
        }

        public async Task<User> GetMemberAsync(Guid memberId)
        {
            return await _context.Users
                .Include(u => u.Church)
                .FirstOrDefaultAsync(u => u.Id == memberId.ToString());
        }

        public async Task<IdentityResult> ConfirmEmailAsync(User member, string token)
        {
            return await _userManager.ConfirmEmailAsync(member, token);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(User member)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(member);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(User member)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(member);
        }

        public async Task<IdentityResult> ResetPasswordAsync(User member, string token, string password)
        {
            return await _userManager.ResetPasswordAsync(member, token, password);
        }

        public async Task<Church> GetChurchAsync(int idChurch)
        {
            return await _context.Churches
                .FirstOrDefaultAsync(c => c.Id == idChurch);
        }
    }

}
