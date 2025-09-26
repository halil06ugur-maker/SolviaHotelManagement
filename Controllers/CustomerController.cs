using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolviaHotelManagement.Domainn.Infrastructure.Interface.CustomerService;
using SolviaHotelManagement.Models.ViewModels.Customer;

namespace SolviaHotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // Listeleme
        [HttpGet]
        public async Task<IActionResult> GetCustomerListAsync()
        {
            var result = await _customerService.GetCustomerListAsync();
            return Ok(result);
        }

        // Id ile Getir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerByIdAsync(int id)
        {
            var result = await _customerService.GetCustomerByIdAsync(id);
            return Ok(result);
        }

        // Ekle
        [HttpPost]
        public async Task<IActionResult> AddCustomerAsync([FromBody] CustomerViewModel viewModel)
        {
            var result = await _customerService.AddCustomerAsync(viewModel);
            return Ok(result);
        }

        // Güncelle
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerAsync([FromBody] CustomerViewModel viewModel)
        {
            var result = await _customerService.UpdateCustomerAsync(viewModel);
            return Ok(result);
        }

        // Sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerAsync(int id)
        {
            var result = await _customerService.DeleteCustomerAsync(id);
            return Ok(result);
        }
    } 
}
