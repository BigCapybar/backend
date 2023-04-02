using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using backend_baz_lab.Models;
using backend_baz_lab.Controllers;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;
using backend_baz_lab.Models;

namespace backend_baz_lab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public struct LoginData
        {
            public string login { get; set; }
            public string password { get; set; }
        }

        private readonly FoodDayContext _context;

        public AuthController(FoodDayContext context)
        {
            _context = context;
        }

        private string HashStr(string value)
        {
            var str = Encoding.UTF8.GetBytes(value);
            var sb = new StringBuilder();
            foreach (var b in MD5.HashData(str))
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        [HttpPost]
        public object GetToken([FromBody] LoginData ld)
        {
            ld.password = HashStr(ld.password);
            var user = _context.User.FirstOrDefault(u => u.Login == ld.login && u.PasswordHash == ld.password);
            if (user == null)
            {
                Response.StatusCode = 401;
                return new { message = "wrong login/password" };
            }
            return Authication.GenerateToken(user.IsAdmin);
        }
        [HttpGet("users")]
        public List<User> GetUsers()
        {
            return _context.User.ToList();
        }
        [HttpGet("token")]
        public object GetToken()
        {
            return Authication.GenerateToken();
        }
        [HttpGet("token/secret")]
        public object GetAdminToken()
        {
            return Authication.GenerateToken(true);
        }
    }
}