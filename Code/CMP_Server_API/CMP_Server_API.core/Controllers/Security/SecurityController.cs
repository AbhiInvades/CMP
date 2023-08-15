using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Linq;
using CMP_Server_API.CMP_Server_API.Infra.Services.Utility;
using CMP_Server_API.CMP_Server_API.Data.Models.Security;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CMP_Server_API.CMP_Server_API.core.Controllers.Security
{
    [ApiController]
    [Route("")]
    public class SecurityController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly string _name;
        private readonly string _secret;
        private readonly IEncryptor _encryptor;

        public SecurityController(IConfiguration config, IEncryptor encryptor)
        {
            _config = config;
            _name = "Abhishek";
            _secret = "1234";
            _encryptor = encryptor;
        }

        [HttpPost("login")]
        public async Task<ActionResult> login([FromBody] User user)
        {
            if (user.username == _name && user.Password == _secret)
            {
                //CookieOptions cookieOptionsAuth = new CookieOptions();
                //cookieOptionsAuth.Secure = false;
                //cookieOptionsAuth.HttpOnly = false;
                //cookieOptionsAuth.Domain = ".localhost:4200";
                ////cookieOptionsAuth.Domain = "localhost";
                ////cookieOptionsAuth.IsEssential = true;
                DateTime date;
                if (user.isPersistent)
                {
                    date = DateTime.Now.AddYears(1);
                }
                else
                {
                    date = DateTime.Now.AddMinutes(60);
                }
               // cookieOptionsAuth.Expires = date;
               var token = GenerateJWTToken(user, date);
               // //var expiryCookie = new CookieHeaderValue("expiry", date.ToString());
               // //CookieOptions cookieOptionsExpiry = new CookieOptions();
               // //cookieOptionsExpiry.Domain = "localhost";
               // Response.Cookies.Append("auth", token, cookieOptionsAuth);
               // Response.StatusCode = 200;
               //// Response.Cookies.Append("expiry",date.ToString(), cookieOptionsExpiry);
               // await Response.StartAsync();
               return Ok(new AccessResult(token, date.ToString())); 
            }
            else
            {
                //Response.StatusCode = 400;
                //await Response.StartAsync();
                return BadRequest();
            }
        }

        public string GenerateJWTToken(User userInfo, DateTime date)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt")["SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.username),
                new Claim("fullName", userInfo.username),
                new Claim("role",userInfo.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var token = new JwtSecurityToken(
                issuer: _config.GetSection("Jwt")["Issuer"],
                audience: _config.GetSection("Jwt")["Audience"],
                claims: claims,
                expires: date,
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class User
    {
        public string username { get; set; }
        public string Password { get; set; }

        public bool isPersistent { get; set; }
        public string Role { get; set; }
        public User(string name, string password, string role, bool isPersistent)
        {
            username = name;
            Password = password;
            Role = role;
            this.isPersistent = isPersistent;
        }
        public User() { }
    }
}
