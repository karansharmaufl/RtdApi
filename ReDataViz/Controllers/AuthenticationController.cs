using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace ReDataViz.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost("register")]
        public JwtData Register([FromBody] Models.User user)
        {
            var jwt = new JwtSecurityToken();
            var encoded_jwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtData {
                Token = encoded_jwt
            };
        }
    }

    public class JwtData
    {
        public string Token { get; set; }
    }
}