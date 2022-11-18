using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Trainin_App_for_Repository.CQRS.Response.Query.User;

namespace Trainin_App_for_Repository.Security
{
    public class CreatingToken
    {
        public static string CreateTokenRegister(UserToken userTokenDto)
        {
            if (userTokenDto != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("SuperStrongOverPowerKey");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, userTokenDto.Id.ToString()),
                        new Claim(ClaimTypes.Name, userTokenDto.Name.ToString()),
                        new Claim(ClaimTypes.Email, userTokenDto.Email.ToString()),
                    }),

                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    Expires = DateTime.Now.AddDays(90)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            else
            {
                return null;
            }
        }
    }
}
