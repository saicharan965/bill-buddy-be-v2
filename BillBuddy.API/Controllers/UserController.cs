using BillBuddy.API.Data;
using BillBuddy.API.DTOs;
using BillBuddy.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BillBuddy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDBContext _appDbContext;

        public UserController(ApplicationDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<ActionResult<UserDetails>> GetUserDetails([FromBody] UserDetails userDetails)
        {
            if (userDetails == null)
            {
                return BadRequest("User details cannot be null.");
            }
            var existingUser = await _appDbContext.Users
                .FirstOrDefaultAsync(u => u.Auth0Identifier == userDetails.Auth0Identifier);

            if (existingUser == null)
            {
                var newUser = new User
                {
                    Auth0Identifier = userDetails.Auth0Identifier,
                    PublicIndentifier = Guid.NewGuid(),
                    UserId = await GetNextUserIdAsync(),
                    FirstName = userDetails.FirstName,
                    LastName = userDetails.LastName,
                    EmailId = userDetails.EmailId,
                    ProfilePictureUrl = userDetails.ProfilePictureUrl,
                    IsDeleted = false,
                    IsActive = true
                };

                _appDbContext.Users.Add(newUser);
                await _appDbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetUserDetails), new { id = newUser.PublicIndentifier }, MapToUserDetails(newUser));
            }

            return Ok(MapToUserDetails(existingUser));
        }

        private async Task<int> GetNextUserIdAsync()
        {
            return await _appDbContext.Users.CountAsync() + 1;
        }

        private UserDetails MapToUserDetails(User user)
        {
            return new UserDetails
            {
                Auth0Identifier = user.Auth0Identifier,
                PublicIndentifier = user.PublicIndentifier,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailId = user.EmailId,
                ProfilePictureUrl = user.ProfilePictureUrl,
                IsDeleted = user.IsDeleted,
                IsActive = user.IsActive
            };
        }
    }
}
