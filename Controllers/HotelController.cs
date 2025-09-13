using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SolviaHotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        [HttpGet]
        public IActionResult get()
        {
            return Ok("Hotel");
        }
    }
}
