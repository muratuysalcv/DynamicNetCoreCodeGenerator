using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Scriptingo.Common;
using Scriptingo.Common.Models;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Claims;

namespace Scriptingo.Admin.Managers
{
    public class SessionManager
    {
        public static void SetSession(long userID, HttpContext _httpContext)
        {
            var config = AdminConfig.Get();
            var token =
                Tokenizer.GenerateToken(
                config.Jwt.Key,
                config.Jwt.Issuer,
                config.Jwt.Audience,
                new List<Claim> { new Claim("user_id", userID + "") },
                int.MaxValue);

            _httpContext.Session.SetString("user", userID + "");
            _httpContext.Session.SetString("token", token + "");
            

            //var dbUser = new FastApiContext<_user>();
        }

        public string Token(HttpContext _http)
        {
            return _http.Session.GetString("token");
        }

        public static void Logout(HttpContext _httpContext)
        {
            Kill("user", _httpContext);
            Kill("token", _httpContext);
        }
        public static long? GetUserId(HttpContext _httpContext)
        {
            var userIDtext = _httpContext.Session.GetString("user") + "";
            if (string.IsNullOrEmpty(userIDtext))
            {
                return null;
            }
            else
            {
                return Convert.ToInt64(userIDtext);
            }
        }
        public static _user GetUser(HttpContext _httpContext)
        {
            try
            {

                var userIDtext = _httpContext.Session.GetString("user") + "";
                if (string.IsNullOrEmpty(userIDtext))
                {
                    return null;
                }
                else
                {
                    var userId = Convert.ToInt64((userIDtext));
                    var dbUser = new FastApiContext<_user>();
                    var user = dbUser.Data.FirstOrDefault(x => x.ID == userId);
                    return user;
                }

            }
            catch (Exception)
            {
                return null;
            }
        }
        public static bool IsLogined(HttpContext _httpContext)
        {
            return SessionManager.GetUser(_httpContext) != null;
        }

        public static void SetObject<T>(T obj, string key, HttpContext _httpContext)
        {
            _httpContext.Session.SetString("messages", JsonConvert.SerializeObject(obj));
        }
        public static T GetObject<T>(string key, HttpContext _httpContext)
        {
            var json = _httpContext.Session.GetString(key)+"";
            return JsonConvert.DeserializeObject<T>(json);
        }
        public static void Kill(string key, HttpContext _httpContext)
        {
            _httpContext.Session.Remove(key);
        }

    }
}
