using BillBuddy.API.Data;
using BillBuddy.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace BillBuddy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly ApplicationDBContext _appDbContext;
        public UserController(ApplicationDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public List<User> GetALlUsers()
        {
            return _appDbContext.Users.ToList();
        }


        [HttpGet("{id}")]
        public ActionResult<User> GetALlUsers(int id)
        {
            try
            {
                var user = _appDbContext.Users.FirstOrDefault(user => user.Id == id);
                if (user == null) return NotFound();
                else return Ok(user);
            }
            catch (Exception)
            {
                throw;
            }

        }


        [HttpPost("createUser")]
        public ActionResult<User> AddUser([FromBody] User user)
        {
            try
            {
                bool userExists = _appDbContext.Users.Any(u => u.EmailId == user.EmailId);
                if (!userExists)
                {
                    _appDbContext.Users.Add(user);
                    _appDbContext.SaveChanges();
                    return Ok(user);
                }
                return Conflict();

            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpPut("{id}")]
        public ActionResult<User> UpdateUser([FromBody] User user, int id)
        {
            try
            {
                var userToUpdate = _appDbContext.Users.FirstOrDefault(u => u.Id == id);
                if (userToUpdate == null)
                {
                    return NotFound();
                }
                userToUpdate.Name = user.Name;
                userToUpdate.EmailId = user.EmailId;
                userToUpdate.ProfilePictureUrl = user.ProfilePictureUrl;

                _appDbContext.SaveChanges();
                return Ok(userToUpdate);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<User> DeleteUser(int id)
        {
            try
            {
                var userToDelete = _appDbContext.Users.FirstOrDefault(u => u.Id == id);
                if (userToDelete == null)
                {
                    return NotFound();
                }
                _appDbContext.Users.Remove(userToDelete);
                _appDbContext.SaveChanges();
                return Ok(userToDelete);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
