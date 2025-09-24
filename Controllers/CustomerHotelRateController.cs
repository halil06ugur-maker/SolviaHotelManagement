using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolviaHotelManagement.Domainn.Infrastructure.Interface.CustomerHotelRate;
using SolviaHotelManagement.Models.ViewModels.CustomerHotelRate;

namespace SolviaHotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerHotelRateController : ControllerBase
    {
        private readonly ICustomerHotelRateService _rateService;

        public CustomerHotelRateController(ICustomerHotelRateService rateService)
        {
            _rateService = rateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRateListAsync()
        {
            var result = await _rateService.GetRateListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRateByIdAsync(int id)
        {
            var result = await _rateService.GetRateByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddRateAsync([FromBody] CustomerHotelRateViewModel viewModel)
        {
            var result = await _rateService.AddRateAsync(viewModel);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRateAsync([FromBody] CustomerHotelRateViewModel viewModel)
        {
            var result = await _rateService.UpdateRateAsync(viewModel);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRateAsync(int id)
        {
            var result = await _rateService.DeleteRateAsync(id);
            return Ok(result);
        }
    }
}
