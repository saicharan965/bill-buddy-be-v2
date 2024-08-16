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

        [HttpPut]
        public async Task<ActionResult<UserDetails>> Update([FromBody] UserDetails userDetails, CancellationToken cancellationToken)
        {
            var userToUpdate = await _appDbContext.Users.FirstOrDefaultAsync(u => u.PublicIndentifier == userDetails.PublicIndentifier, cancellationToken);
            if (userToUpdate == null)
            {
                return NotFound($"The user with ID {userDetails.PublicIndentifier} was not found.");
            }
            userToUpdate.ProfilePictureUrl = userDetails.ProfilePictureUrl;
            userToUpdate.FirstName = userDetails.FirstName;
            userToUpdate.LastName = userDetails.LastName;
            userToUpdate.EmailId = userDetails.EmailId;
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return Ok(MapToUserDetails(userToUpdate));
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<UserDetails>> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            if (id.Equals(Guid.Empty))
            {
                return BadRequest("User Id cannot be empty GUID.");
            }
            var userToDelete = await _appDbContext.Users.FirstOrDefaultAsync(u => u.PublicIndentifier == id, cancellationToken);
            if (userToDelete == null)
            {
                return NotFound($"The user with ID {id} was not found.");
            }
            userToDelete.IsDeleted = true;
            _appDbContext.SaveChanges();
            return Ok(userToDelete);
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

// We use "cancellationToken" just like we use takeUntil() for unsubscribe.
// It stops all the async tasks when the request has been cancelled.We use this to aviod memory leaks.
//For example, Frontend makes req, then cancells.But we continue to execute a task.But in this case, if we make cancellationToken available, We can stop the async calls

// use Task.WhenAll() for multi-threaded programming.It makes Async calls like Fork join in Rxjs. For example :
//var userTask = _appDbContext.Users.FirstOrDefaultAsync(u => u.PublicIndentifier == id, cancellationToken);
//var userTask1 = _appDbContext.Users.FirstOrDefaultAsync(u => u.PublicIndentifier == id, cancellationToken);
//var userTask2 = _appDbContext.Users.FirstOrDefaultAsync(u => u.PublicIndentifier == id, cancellationToken);
//var userss = await Task.WhenAll(userTask, userTask1, userTask2);
