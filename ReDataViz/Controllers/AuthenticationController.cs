using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace ReDataViz.Controllers
{
    public class JwtData
    {
        public string FirstName { get; set; }
        public string Token { get; set; }
        public string EmailID { get; set; }
    }

    public class LoginData
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }


    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        readonly ApiContext _context;

        // Build a constructor to get the API reference 

        public AuthenticationController(ApiContext _context)
        {
            this._context = _context;
        }

        [HttpPost("register")]
        public JwtData Register([FromBody] Models.User user)
        {

            _context.Users.Add(user);
            _context.SaveChanges();
            var jwtData = CreateJwtData(user);
            return jwtData;
        }

        [HttpPost("login")]
        public JwtData Login([FromBody] LoginData loginData) // Taking a different body to stop confusion
        {
            var user = _context.Users.SingleOrDefault(usr => usr.EmailID.Equals(loginData.Email) && usr.Passsword.Equals(loginData.Password));
            return CreateJwtData(user);
        }

        private JwtData CreateJwtData(Models.User user)
        {
            var jwt = new JwtSecurityToken();
            var encoded_jwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            
            // catch after post request
            return new JwtData
            {
                Token = encoded_jwt,
                FirstName = user.FirstName,
                EmailID = user.EmailID
            };
        }
    }
}

    

    