using Microsoft.AspNetCore.Mvc;
using WAD.Backend._00015641.Data;
using WAD.Backend._00015641.Models;

namespace WAD.Backend._00015641.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly AppDbContext _context;

        public AuthenticationController(ILogger<AuthenticationController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var user = _context.Users.SingleOrDefault(u => u.Name == request.Username);

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
            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User registered successfully");
        }
    }

    // Request DTOs
    public record LoginRequest(string Username, string Password);
    public record RegisterRequest(string Username, string Password, string Email);
}
