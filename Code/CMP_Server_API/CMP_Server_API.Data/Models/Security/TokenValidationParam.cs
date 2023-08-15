using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CMP_Server_API.Models.Security
{
    public class TokenValidationParam : TokenValidationParameters
    {
        public TokenValidationParam(IConfiguration configuration):base()
        {
            ValidateIssuer = true;
            ValidateAudience = true;
            ValidateLifetime = true;
            ValidateIssuerSigningKey = true;
            ValidIssuer = configuration.GetSection("Jwt")["Issuer"];
            ValidAudience = configuration.GetSection("Jwt")["Audience"];
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt")["SecretKey"]));
            ClockSkew = TimeSpan.Zero;
        }
    }
    
}
