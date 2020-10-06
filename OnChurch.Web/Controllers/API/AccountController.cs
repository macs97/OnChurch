using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnChurch.Web.Data.Entities;
using OnChurch.Web.Helpers;
using OnChurch.Web.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using OnChurch.Common.Requests;

namespace OnChurch.Web.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserHelper _userHelper;
        private readonly IConfiguration _configuration;

        public AccountController(IUserHelper userHelper, IConfiguration configuration)
        {
            _userHelper = userHelper;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("CreateToken")]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User member = await _userHelper.GetMemberAsync(model.Username);
                if (member != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await _userHelper.ValidatePasswordAsync(member, model.Password);

                    if (result.Succeeded)
                    {
                        Claim[] claims = new[]
                        {
                        new Claim(JwtRegisteredClaimNames.Sub, member.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

                        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        JwtSecurityToken token = new JwtSecurityToken(
                            _configuration["Tokens:Issuer"],
                            _configuration["Tokens:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddDays(99),
                            signingCredentials: credentials);
                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,
                            member
                        };

                        return Created(string.Empty, results);
                    }
                }
            }

            return BadRequest();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("UpdateMember")]
        public async Task<IActionResult> UpdateMember([FromBody] UpdateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            User member = await _userHelper.GetMemberAsync(request.UserName);
            if (member == null)
            {
                return NotFound("Error001");
            }
            member.Document = request.Document;
            member.FirstName = request.FirstName;
            member.LastName = request.LastName;
            member.Address = request.Address;
            member.PhoneNumber = request.PhoneNumber;
            member.IdProfession = request.IdProfession;
            member.Church = await _userHelper.GetChurchAsync(request.IdChurch);

            await _userHelper.UpdateMemberAsync(member);

            return Ok(member);
        }

    }

}
