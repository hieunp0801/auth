using auth.Data;
using auth.Models;
using auth.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

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
        
        public AuthController(MyDbContext context){
            _context = context;
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
                    ResponseObject response = new ResponseObject{
                        status = "200",
                        message = "Login success",
                        data = null
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