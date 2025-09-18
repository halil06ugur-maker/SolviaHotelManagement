using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolviaHotelManagement.Domainn.Infrastructure.Interface.HotelRoomService;
using SolviaHotelManagement.Models.ViewModels.HotelRoom;

namespace SolviaHotelManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelRoomController : ControllerBase
    {
        private readonly IHotelRoomService _hotelRoomService;

        public HotelRoomController(IHotelRoomService hotelRoomService)
        {
            _hotelRoomService = hotelRoomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var result = await _hotelRoomService.GetListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _hotelRoomService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("byhotel/{hotelId}")]
        public async Task<IActionResult> GetListByHotelId(int hotelId)
        {
            var result = await _hotelRoomService.GetListByHotelIdAsync(hotelId);
            return Ok(result);
        }

        [HttpGet("byroom/{roomId}")]
        public async Task<IActionResult> GetListByRoomId(int roomId)
        {
            var result = await _hotelRoomService.GetListByRoomIdAsync(roomId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] HotelRoomViewModel viewModel)
        {
            var result = await _hotelRoomService.AddAsync(viewModel);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] HotelRoomViewModel viewModel)
        {
            var result = await _hotelRoomService.UpdateAsync(viewModel);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _hotelRoomService.DeleteAsync(id);
            return Ok(result);
        }
    }

}
