using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Scriptingo.Common
{
    public class Tokenizer
    {
        public static string GenerateToken(string key,string issuer,string audience,List<Claim> claims, int timeoutMinute=10)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.Now.AddMinutes(timeoutMinute),
                signingCredentials: signinCredentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return jwt_token;
        }
    }
}
