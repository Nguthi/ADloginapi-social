using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using ADloginAPI.Data;
using ADloginAPI.Models;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;


namespace ADloginAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public AuthController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User model)
        {
            if (!ModelState.IsValid) { 
                return BadRequest(ModelState);
            }

            //check if user is already registered

            if(await _dbContext.Users.AnyAsync(u=> u.Email == model.Email))
            {
                return BadRequest("Email is already registered.");
            }
            //hash password
            model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

            _dbContext.Users.Add(model);
            await _dbContext.SaveChangesAsync();

            return Ok("User Registered succesfully");

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(User model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //find the email in the database
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            
            if (user == null)
            {
                return NotFound("User Not found");
            }

            //check if passwords match

            if(!BCrypt.Net.BCrypt.Verify(model.Password, user.Password)) {
                return BadRequest("Invalid Password");
            }

            return Ok("Login Succesful");

        }
    }
}
