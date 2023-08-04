﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JWT_Token_01.Model;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace JWT_Token_01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        public LoginController(IConfiguration configuration)
        {
            _config = configuration;
        }
        private Users Authenticationuser(Users user)
        {
            Users _user = null;
            if(user.Username == "admin" && user.Password=="12345") { 
            _user =new Users { Username="mohit shukla"};
            }
            return _user;
        }

        private string GenerateToken(Users user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"],null,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
       
        
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(Users user)
        {
            IActionResult response = Unauthorized();
            var user_ = Authenticationuser(user);
            if (user_ != null)
            {
                var token= GenerateToken(user_);
                response= Ok(new { token = token });
            }
            return response;
        }
    }

}
