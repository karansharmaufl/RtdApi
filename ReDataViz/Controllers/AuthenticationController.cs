using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ReDataViz.Controllers
{
    public class JwtData
    {
        public string FirstName { get; set; }
        public string Token { get; set; }
        public string EmailID { get; set; }
        public string AckMessage { get; set; }
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

        //[HttpGet]
        //public IEnumerable<Models.User> AllUsers()
        //{
        //    return _context.Users;
        //}

        [HttpPost("register")]
        public JwtData Register([FromBody] Models.User user)
        {
            var listEmails = _context.Users.Where(usr => usr.EmailID.Equals(user.EmailID));
            
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return new JwtData
                {
                    Token = "",
                    FirstName = "",
                    EmailID = "",
                    AckMessage=e.GetType().ToString()
                };
            }
            
            var jwtData = CreateJwtData(user);
            return jwtData;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginData loginData) // Taking a different body to stop confusion
        {
            var user = _context.Users.SingleOrDefault(usr => usr.EmailID.Equals(loginData.Email) 
            && usr.Passsword.Equals(loginData.Password));

            if (user == null)
                return NotFound("Email or password incorrect try again");
            return Ok(CreateJwtData(user));
        }

        private JwtData CreateJwtData(Models.User user)
        {
            var SIGN_IN_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is the secret phrase"));
            var singingCredentials = new SigningCredentials(SIGN_IN_KEY, SecurityAlgorithms.HmacSha256);
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.EmailID)
            };

            
            var jwt = new JwtSecurityToken(claims : claims, signingCredentials: singingCredentials); // Adding claims for security
            var encoded_jwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            
            // catch after post request
            return new JwtData
            {
                Token = encoded_jwt,
                FirstName = user.FirstName,
                EmailID = user.EmailID,
                AckMessage = ""
            };
        }
    }
}

    

    