using LeaveApp_api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeaveApp_api.Controllers
{
   // [Authorize] // Only requests with valid JWT can call this
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        /// <summary>
        /// Get all active users
        /// </summary>
        /// <returns>List of active users</returns>
        /// 
        [HttpGet("active")]
        public async Task<IActionResult> GetActiveUsers()
        {
            var users = await _userRepo.GetActiveUsersAsync();
            return Ok(users);
        }
    }
}
