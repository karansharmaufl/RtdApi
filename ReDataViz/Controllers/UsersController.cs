using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReDataViz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApiContext _context; 
        public UsersController(ApiContext _context)
        {
            this._context = _context;
        }
        [HttpGet]
        public IEnumerable<Models.User> Get()
        {
            return _context.Users;
        }

        [HttpGet("{id}")]
        public ActionResult<Models.User> GetUserById(string id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound("User Not Found");
            return Ok(user);
            
        }
        // Using auth middleware
        [Authorize]
        [HttpGet("me")]
        public ActionResult GetUser()
        {
            var id = HttpContext.User.Claims.First().Value;
            return Ok(_context.Users.SingleOrDefault( u => u.Id == id ));
        }

    }
}