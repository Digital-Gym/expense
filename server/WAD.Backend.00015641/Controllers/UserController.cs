using Microsoft.AspNetCore.Mvc;
using WAD.Backend._00015641.Data.Repositories;
using WAD.Backend._00015641.Models;

// this controller only for debuggin purposes
// will be deleted or rewritten

namespace WAD.Backend._00015641.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }


        // GET: api/User/4
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
    }
}
