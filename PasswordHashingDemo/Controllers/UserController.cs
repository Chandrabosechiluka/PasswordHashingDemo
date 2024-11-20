using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PasswordHashingDemo.Data;
using PasswordHashingDemo.DTOs;
using PasswordHashingDemo.Models;

namespace PasswordHashingDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly MyContext _context;
        public UserController(MyContext context)
        {
            _context = context;
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(UserDto userDto)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            var user = new User
            {
                UserName = userDto.UserName,
                PasswordHash = passwordHash,
                Email = userDto.Email
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok("User created successfully.");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == loginDto.UserName);
            if (user == null) return BadRequest("Invalid credentials.");

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
            if (!isPasswordValid) return BadRequest("Invalid credentials.");

            return Ok("Login successful.");
        }

    }

}
