using Microsoft.AspNetCore.Mvc;
using WAD.Backend._00015641.Data.Repositories;
using WAD.Backend._00015641.Models;

namespace WAD.Backend._00015641.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IUserRepository _userRepository;

        public AuthenticationController(ILogger<AuthenticationController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _userRepository.GetUserByName(request.Username);

            if (user == null)
            {
                _logger.LogWarning("Login attempt failed for {Username}: User not found", request.Username);
                return Unauthorized("User not found");
            }

            if (user.Password != request.Password)
            {
                _logger.LogWarning("Login attempt failed for {Username}: Incorrect password", request.Username);
                return Unauthorized("Invalid credentials");
            }

            // on success
            _logger.LogInformation("User {Username} logged in successfully", request.Username);
            return Ok("Login successful");
        }


        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            _logger.LogInformation("Registration attempt for {Username}", user.Name);

            if (string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest("Password cannot be empty");
            }

            // todo: encrypt password with sha256 or smth (64 bytes long)
            _userRepository.AddUserAsync(user);

            return Ok("User registered successfully");
        }
    }

    // Request DTOs
    public record LoginRequest(string Username, string Password);
    public record RegisterRequest(string Username, string Password, string Email);
}
