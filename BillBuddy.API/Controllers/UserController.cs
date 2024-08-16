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
    }
}
