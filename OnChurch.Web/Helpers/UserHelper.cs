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
        private readonly UserManager<Member> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<Member> _signInManager;

        public UserHelper(DataContext context, UserManager<Member> userManager, RoleManager<IdentityRole> roleManager, SignInManager<Member> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> AddMemberAsync(Member member, string password)
        {
            return await _userManager.CreateAsync(member, password);
        }

        public async Task AddMemberToRoleAsync(Member member, string roleName)
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

        public async Task<Member> GetMemberAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Church)
                .Include(u => u.Profession)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> IsMemberInRoleAsync(Member member, string roleName)
        {
            return await _userManager.IsInRoleAsync(member, roleName);
        }

        public async Task<Member> AddMemberAsync(AddMemberViewModel model, Guid photoId, UserType userType)
        {
           
            Member member = new Member
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

            Member newMember = await GetMemberAsync(model.Username);
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

    }

}
