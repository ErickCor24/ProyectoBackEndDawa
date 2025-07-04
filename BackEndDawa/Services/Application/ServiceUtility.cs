using BackEndDawa.Models;
using Microsoft.IdentityModel.Tokens;
using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackEndDawa.Services.Application
{
    public class ServiceUtility
    {

        private readonly IConfiguration _configuration;

        public ServiceUtility(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateUserJwtToken(Object user, string role)
        {

            //Get security key from configuration (appsettings.json: jwtSettings: key)
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_configuration["JwtSettings:key"]!));

            //Create the type of encrypt to the security key
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create token details

            try
            {
                var jwtConfig = new JwtSecurityToken(
                    claims: CreateClaims(user, role),
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: credentials
                    );
                return new JwtSecurityTokenHandler().WriteToken(jwtConfig);

            } catch (ArgumentException)
            {
                return string.Empty;
            }

        }

        private Claim[] CreateClaims(Object user, string role)
        {
            if (user is UserCompany userCompany)
            {
                return
                [
                new Claim("id", userCompany.CompanyId.ToString()),
                new Claim("role", role)
                ];
            }
            else if (user is UserClient userClient)
            {
                return
                [               
                new Claim("id", userClient.ClientId.ToString()),
                new Claim("role", role)
                ];
            }
            else
            {
                throw new ArgumentException("Unsupported user type", nameof(user));
            }

        }


    }
}
