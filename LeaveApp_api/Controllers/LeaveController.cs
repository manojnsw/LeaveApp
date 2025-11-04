using LeaveApp_api.Models;
using LeaveApp_api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;

using System.Threading.Tasks;
namespace LeaveApp_api.Controllers
{
  //  [Authorize] // Only requests with valid JWT can call this
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveController : ControllerBase
    {
        private readonly ILeaveFormRepository _leaveRepo;
        private readonly IUserRepository _userRepo;

        public LeaveController(ILeaveFormRepository leaveRepo, IUserRepository userRepo)
        {
            _leaveRepo = leaveRepo;
            _userRepo = userRepo;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepo.GetActiveUsersAsync();
            return Ok(users);
        }

        [HttpPost("submit")]
        public async Task<IActionResult> Post([FromBody] LeaveForm leave)
        {
            if (leave.ApplicantId == leave.ManagerId)
                return BadRequest("Applicant and Manager cannot be the same.");
            if (leave.StartDate.Date < DateTime.Today)
                return BadRequest("Start date cannot be in the past.");
            if (leave.EndDate <= leave.StartDate)
                return BadRequest("End date must be after start date.");
            if (leave.ReturnDate <= leave.EndDate)
                return BadRequest("Return date must be after end date.");
            if (!string.IsNullOrEmpty(leave.GeneralComments) && leave.GeneralComments.Length > 500)
                return BadRequest("Comments cannot exceed 500 characters.");

            try
            {
                await _leaveRepo.SubmitLeaveAsync(leave);
                return Ok("Leave submitted successfully.");
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
