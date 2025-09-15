using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolviaHotelManagement.Domainn.Infrastructure.Interface.HotelService;
using SolviaHotelManagement.Domainn.Infrastructure.Service.HotelService;
using SolviaHotelManagement.Models.VİewModels.Hotel;

namespace SolviaHotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _HotelService;

        public HotelController(IHotelService hotelService)
        {
            _HotelService = hotelService;
        }


        [HttpGet]
        public async Task<IActionResult> GetHotelListAsync()
        {
            var result = await _HotelService.GetHotelListAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddHotelAsync([FromBody] HotelViewModel ViewModel)
        {
            var result = await _HotelService.AddHotelAsync(ViewModel);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateHotelAsync([FromBody] HotelViewModel ViewModel)
        {
            var result=await _HotelService.UpdateHotelAsync(ViewModel);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteHotelAsync(int id)
        {
            var result = await _HotelService.DeleteHotelAsync(id);
            return Ok(result);
        }
    }
}
