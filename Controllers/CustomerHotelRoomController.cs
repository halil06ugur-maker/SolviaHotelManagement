using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolviaHotelManagement.Domainn.Infrastructure.Interface.CustomerHotelRoomService;
using SolviaHotelManagement.Models.ViewModels.CustomerHotelRoom;

namespace SolviaHotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerHotelRoomController : ControllerBase
    {
        private readonly ICustomerHotelRoomService _reservationService;

        public CustomerHotelRoomController(ICustomerHotelRoomService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetReservationListAsync()
        {
            var result = await _reservationService.GetReservationListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationByIdAsync(int id)
        {
            var result = await _reservationService.GetReservationByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddReservationAsync([FromBody] CustomerHotelRoomViewModel viewModel)
        {
            var result = await _reservationService.AddReservationAsync(viewModel);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReservationAsync([FromBody] CustomerHotelRoomViewModel viewModel)
        {
            var result = await _reservationService.UpdateReservationAsync(viewModel);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservationAsync(int id)
        {
            var result = await _reservationService.DeleteReservationAsync(id);
            return Ok(result);
        }
    }
}
