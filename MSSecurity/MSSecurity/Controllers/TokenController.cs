using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MSSecurity.Service;
using MSSecurity.ViewObject;

namespace MSSecurity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public TokenController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<ActionResult> Post(UserRequest request)
        {
            var res = await _userService.GetAll();

            var user = res.Where(whr => whr.login.Equals(request.Login) && whr.password.Equals(request.Password)).FirstOrDefault();
            if (user == null)
            {
                return Unauthorized();
            }
            Response.Headers.Add("access-control-expose-headers", "Authorization");
            Response.Headers.Add("Authorization", "Bearer " + CreateToken());
            return Ok();
        }
        private string CreateToken()
        {
            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["token:key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_configuration["token:issuer"], _configuration["token:audience"],
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["token:expiration_minutes"])),
                    signingCredentials: creds);
                string _token = new JwtSecurityTokenHandler().WriteToken(token);

                return _token;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}