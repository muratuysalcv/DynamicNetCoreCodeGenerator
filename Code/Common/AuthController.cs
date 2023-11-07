using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Validations;
using Scriptingo.Common;
using Scriptingo.Common.Models;
using Scriptingo.FastApi;
using Scriptingo.FastApi.Common;

namespace Scriptingo.Common
{
    [Route("api/[controller]/[action]")]
    public class AuthController : Controller
    {
        [HttpPost("Login")]
        public ApiReturn Login([FromBody] LoginRequest req)
        {
            var res = new ApiReturn();
            var dbApi = new FastApiContext<_api>();
            var dbProcess = new FastApiContext<_process>();
            var dbCon = new FastApiContext<_con>();

            var api = dbApi.Data.FirstOrDefault(x => x.name == req.Api);
            var paramters = new List<data> {
                new data() { name="Email",value=req.Email},
                new data() { name="Password",value= CreateMD5(req.Password)}};


            var config = Config.Get();
            // JWT
            string key = config.Jwt.Key; //Secret key which will be used later during validation    
            var issuer = config.Jwt.Issuer;  //normally this will be your site URL    
            var audience = config.Jwt.Audience;

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer,
                audience,
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(6),
                signingCredentials: signinCredentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            paramters.Add(new data() { name = "JWT", value = jwt_token });

            var userLoginSuccess = 0;

            var loginSql = "select count(*) from user where email='{PREFIX}Email' and password={PREFIX}Password";

            var modelConfig = new ModelConfiguration("Main");
            if (!string.IsNullOrEmpty(loginSql))
            {

                var loginSqlExecuter = new RawSqlExecuter(loginSql, paramters, modelConfig);
                var dbSqlExecuter = new FastApiSqlExecuterContext(modelConfig);
                userLoginSuccess = Convert.ToInt32(loginSqlExecuter.Execute().Rows[0][0] + "");
            }

            if (userLoginSuccess > 0)
            {
                res.status = "success";
                res.data = jwt_token;
            }
            else
            {
                res.status = "error";
                res.message = "Please check your credentials.";
            }
            return res;
        }

        [HttpPost("Register")]
        public ApiReturn Register([FromBody] RegisterRequest req)
        {
            var config = Config.Get();
            var res = new ApiReturn();
            var dbUser = new FastApiContext<_user>("Main");
            var errors = new List<string>();
            if(req.RegisterToken!=config.RegisterToken)
            {
                errors.Add("Register token is not valid.");
            }
            if (string.IsNullOrEmpty(req.Email))
            {
                errors.Add("Email is required field.");
            }
            if (string.IsNullOrEmpty(req.Password))
            {
                errors.Add("Email is required field.");
            }
            else if (dbUser.Data.Count(x => x.e_mail == req.Email) > 0)
            {
                errors.Add("Email alread registered, you can reset password.");
            }
            else if (req.Password.Length < 6)
            {
                errors.Add("Password must be minimum 6 digits.");
            }
            if (string.IsNullOrEmpty(req.FirstName))
            {
                errors.Add("FirstName is required field.");
            }
            if (string.IsNullOrEmpty(req.LastName))
            {
                errors.Add("LastName is required field.");
            }

            if (errors.Count == 0)
            {
                try
                {

                    dbUser.Data.Add(new _user()
                    {
                        e_mail = req.Email,
                        first_name = req.FirstName,
                        last_name = req.LastName,
                        password = CreateMD5(req.Password),
                        status = 1,
                        uid = Guid.NewGuid(),
                        temp_code = (new Random()).Next(100000, 999999)+""
                    });
                    dbUser.SaveChanges();

                }
                catch (Exception ex)
                {
                    errors.Add(ex.Message);
                }
            }

            if(errors.Count>0)
            {
                res.status = "error";
                res.message = string.Join(",", errors);
            }
            else
            {
                res.status = "success";
            }
            return res;
        }

        private string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return Convert.ToHexString(hashBytes);
            }
        }

    }
}
