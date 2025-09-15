using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolviaHotelManagement.Domainn.Infrastructure.Interface.HotelAdressService;
using SolviaHotelManagement.Models.VİewModels.HotelAdress;

namespace SolviaHotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelAddressController : ControllerBase
    {
        private readonly IHotelAddressService _hotelAddressService;

        public HotelAddressController(IHotelAddressService hotelAddressService)
        {
            _hotelAddressService = hotelAddressService;
        }

        [HttpGet]
        public async Task<IActionResult> GetHotelAddressListAsync()
        {
            var result = await _hotelAddressService.GetListAsync();
            return Ok(result);
        }

        [HttpGet("{hotelId}")]
        public async Task<IActionResult> GetHotelAddressByHotelIdAsync(int hotelId)
        {
            var result = await _hotelAddressService.GetByHotelIdAsync(hotelId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddHotelAddressAsync([FromBody] HotelAddressViewModel viewModel)
        {
            var result = await _hotelAddressService.AddHotelAddressAsync(viewModel);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHotelAddressAsync([FromBody] HotelAddressViewModel viewModel)
        {
            var result = await _hotelAddressService.UpdateHotelAddressAsync(viewModel);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelAddressAsync(int id)
        {
            var result = await _hotelAddressService.DeleteHotelAddressAsync(id);
            return Ok(result);
        }

    }
}
