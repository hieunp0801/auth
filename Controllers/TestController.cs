using auth.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController:ControllerBase
    {
        private readonly MyDbContext _context;
        public TestController(MyDbContext context){
            _context = context;
        }
        [HttpGet]
        [Authorize]
        public ActionResult testAuthorize(){
            return Ok("ok");
        }
        
    }
}