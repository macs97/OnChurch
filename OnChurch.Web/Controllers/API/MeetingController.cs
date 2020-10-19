using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnChurch.Common.Requests;
using OnChurch.Common.Responses;
using OnChurch.Web.Data;
using OnChurch.Web.Data.Entities;
using OnChurch.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnChurch.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        public MeetingController(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        //[Route("GetMeetings")]
        public async Task<IActionResult> GetMeeting()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return NotFound();
            }
            User user = await _userHelper.GetMemberAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            List<Meeting> meeting = await _context.Meetings
                .Include(m => m.Assistances)
                .ThenInclude(a => a.User).ToListAsync();
                //.Where(m => m.Church == user.Church).ToListAsync();
            if (meeting == null)
            {
                return NotFound();
            }

            return Ok(meeting); 
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("CreateMeeting")]
        public async Task<IActionResult> CreateMeeting([FromBody] MeetingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "Bad request",
                    Result = ModelState
                });
            }

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "Error003"
                });
            }
            User user = await _userHelper.GetMemberAsync(userId);
            if (user == null)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "Error003"
                });
            }
            Meeting meeting = new Meeting
            {
                Date = request.Date,
                Church = user.Church,
                Assistances = new List<Assistance>()
            };
            List<User> users = await _context.Users.Where(u => u.Church.Id == user.Church.Id && u.UserType != Common.Enum.UserType.Teacher).ToListAsync();
            users.ForEach(user =>
            {
                meeting.Assistances.Add(new Assistance
                {
                    Meeting = meeting,
                    User = user
                });
            });
            _context.Add(meeting);
            await _context.SaveChangesAsync();
            return Ok(new Response { IsSuccess = true });
        }
    }
}
