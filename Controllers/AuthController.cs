using auth.Data;
using auth.Models;
using auth.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController:ControllerBase
    {
        [HttpGet]
        public ActionResult test(){
            return null;
        }
        private readonly MyDbContext _context;
        private readonly IConfiguration _configuration;
        
        public AuthController(MyDbContext context,IConfiguration configuration){
            _context = context;
            _configuration = configuration;
        }
        [HttpPost("login")]
        public ActionResult<ResponseObject> login(LoginModel request){
            string username = request.username;
            string password = request.password;
            var user = _context.users.FirstOrDefault(u => u.username == username);
            if(user == null){
                ResponseObject response = new ResponseObject{
                    status = "404",
                    message = "Username not found.",
                    data = null
                };
                return response;
            }
            else {
                if(UserService.VerifyPassword(password,user.passwordHash)){
                    //  Login successs
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
                    var token = new JwtSecurityToken(
                        expires: DateTime.Now.AddDays(1),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
                    ResponseObject response = new ResponseObject{
                        status = "200",
                        message = "Login success",
                        data = new {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        }
                    };
                    return response;
                }
                else {
                    ResponseObject response = new ResponseObject{
                        status = "400",
                        message = "Password is not corrected.",
                        data = null
                    };
                    return response;
                }

            }
        }
        [HttpPost("register")]
        public ActionResult<ResponseObject> register(RegisterModel request){
            string username = request.username;
            string password = request.password;
            string confirmPassword = request.confirmPassword;
            var user_temp = _context.users.FirstOrDefault(u => u.username == username);
            if(user_temp == null){
                if(password == confirmPassword){
                    User user = new User{
                        username = username,
                        passwordHash =  UserService.HashPassword(password)
                    };
                    _context.users.Add(user);
                    _context.SaveChanges();
                    ResponseObject response = new ResponseObject{
                        status = "200",
                        message = "Register success",
                        data = user
                    };
                    return response;
                }
                else {
                    ResponseObject response = new ResponseObject{
                        status = "400",
                        message = "Password does not match.",
                        data = null
                    };
                    return response;
                }
                
            }
            else {
                ResponseObject response = new ResponseObject{
                    status = "400",
                    message = "Username already exists.",
                    data = null
                };
                return response;
            }
            
        }
    }
}