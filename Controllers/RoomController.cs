using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolviaHotelManagement.Domainn.Infrastructure.Interface.RoomService;
using SolviaHotelManagement.Domainn.Infrastructure.Service.HotelService;
using SolviaHotelManagement.Models.ViewModels.Room;


namespace SolviaHotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoomListAsync()
        {
            var result = await _roomService.GetRoomListAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomByIdAsync(int id)
        {
            var result = await _roomService.GetRoomByIdAsync(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddRoomAsync([FromBody] RoomViewModel viewModel)
        {
            var result = await _roomService.AddRoomAsync(viewModel);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateRoomAsync([FromBody] RoomViewModel viewModel)
        {
            var result = await _roomService.UpdateRoomAsync(viewModel);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomAsync(int id)
        {
            var result = await _roomService.DeleteRoomAsync(id);
            return Ok(result);
        }

    }
}
