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
        public async Task<ActionResult<UserDetailsResponse>> CreateOrGetUser([FromBody] CreateUserRequest userDetails, CancellationToken cancellationToken)
        {
            if (userDetails == null)
            {
                return BadRequest("User details cannot be null.");
            }

            var existingUser = await _appDbContext.Users
                .FirstOrDefaultAsync(u => u.Auth0Identifier == userDetails.Auth0Identifier, cancellationToken);

            if (existingUser == null)
            {
                var newUser = new User
                {
                    Auth0Identifier = userDetails.Auth0Identifier,
                    PublicIdentifier = Guid.NewGuid(),
                    FirstName = userDetails.FirstName,
                    LastName = userDetails.LastName,
                    EmailId = userDetails.EmailId,
                    ProfilePictureUrl = userDetails.ProfilePictureUrl,
                    IsDeleted = false,
                    IsActive = true
                };

                _appDbContext.Users.Add(newUser);
                await _appDbContext.SaveChangesAsync(cancellationToken);

                return CreatedAtAction(nameof(GetUserById), new { id = newUser.PublicIdentifier }, MapToUserDetailsResponse(newUser));
            }

            return Ok(MapToUserDetailsResponse(existingUser));
        }

        [HttpPut]
        public async Task<ActionResult<UserDetailsResponse>> UpdateUser([FromBody] UpdateUserRequest userDetails, CancellationToken cancellationToken)
        {
            var userToUpdate = await _appDbContext.Users
                .FirstOrDefaultAsync(u => u.PublicIdentifier == userDetails.PublicIdentifier, cancellationToken);

            if (userToUpdate == null)
            {
                return NotFound($"The user with ID {userDetails.PublicIdentifier} was not found.");
            }

            userToUpdate.ProfilePictureUrl = userDetails.ProfilePictureUrl;
            userToUpdate.FirstName = userDetails.FirstName;
            userToUpdate.LastName = userDetails.LastName;
            userToUpdate.EmailId = userDetails.EmailId;

            await _appDbContext.SaveChangesAsync(cancellationToken);

            return Ok(MapToUserDetailsResponse(userToUpdate));
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteUser([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            if (id.Equals(Guid.Empty))
            {
                return BadRequest("User Id cannot be empty GUID.");
            }

            var userToDelete = await _appDbContext.Users
                .FirstOrDefaultAsync(u => u.PublicIdentifier == id, cancellationToken);

            if (userToDelete == null)
            {
                return NotFound($"The user with ID {id} was not found.");
            }

            userToDelete.IsDeleted = true;
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserDetailsResponse>> GetUserById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var user = await _appDbContext.Users
                .FirstOrDefaultAsync(u => u.PublicIdentifier == id, cancellationToken);

            if (user == null)
            {
                return NotFound($"User with ID {id} was not found.");
            }

            return Ok(MapToUserDetailsResponse(user));
        }

        private UserDetailsResponse MapToUserDetailsResponse(User user)
        {
            return new UserDetailsResponse
            {
                PublicIdentifier = user.PublicIdentifier,
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
//var userTask = _appDbContext.Users.FirstOrDefaultAsync(u => u.PublicIdentifier == id, cancellationToken);
//var userTask1 = _appDbContext.Users.FirstOrDefaultAsync(u => u.PublicIdentifier == id, cancellationToken);
//var userTask2 = _appDbContext.Users.FirstOrDefaultAsync(u => u.PublicIdentifier == id, cancellationToken);
//var userss = await Task.WhenAll(userTask, userTask1, userTask2);
