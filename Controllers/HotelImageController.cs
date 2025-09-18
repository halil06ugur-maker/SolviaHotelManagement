using Microsoft.AspNetCore.Mvc;
using SolviaHotelManagement.Domainn.Infrastructure.Interface.HotelImageService;
using SolviaHotelManagement.Models.ViewModels.HotelImage;
using System.Threading.Tasks;

namespace SolviaHotelManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelImageController : ControllerBase
    {
        private readonly IHotelImageService _hotelImageService;

        public HotelImageController(IHotelImageService hotelImageService)
        {
            _hotelImageService = hotelImageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var result = await _hotelImageService.GetListAsync();
            return Ok(result);
        }

        [HttpGet("byhotel/{hotelId}")]
        public async Task<IActionResult> GetListByHotelId(int hotelId)
        {
            var result = await _hotelImageService.GetListByHotelIdAsync(hotelId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] HotelImageViewModel viewModel)
        {
            var result = await _hotelImageService.AddHotelImageAsync(viewModel);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] HotelImageViewModel viewModel)
        {
            var result = await _hotelImageService.UpdateHotelImageAsync(viewModel);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _hotelImageService.DeleteHotelImageAsync(id);
            return Ok(result);
        }
    }
}
