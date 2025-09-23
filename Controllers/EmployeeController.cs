using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolviaHotelManagement.Domainn.Infrastructure.Interface.EmployeeService;
using SolviaHotelManagement.Models.ViewModels.Employee;

namespace SolviaHotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        // Listeleme
        [HttpGet]
        public async Task<IActionResult> GetEmployeeListAsync()
        {
            var result = await _employeeService.GetEmployeeListAsync();
            return Ok(result);
        }

        // Id'ye göre getirme
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeByIdAsync(int id)
        {
            var result = await _employeeService.GetEmployeeByIdAsync(id);
            return Ok(result);
        }

        // Ekleme
        [HttpPost]
        public async Task<IActionResult> AddEmployeeAsync([FromBody] EmployeeViewModel viewModel)
        {
            var result = await _employeeService.AddEmployeeAsync(viewModel);
            return Ok(result);
        }

        // Güncelleme
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeeAsync([FromBody] EmployeeViewModel viewModel)
        {
            var result = await _employeeService.UpdateEmployeeAsync(viewModel);
            return Ok(result);
        }

        // Silme
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeAsync(int id)
        {
            var result = await _employeeService.DeleteEmployeeAsync(id);
            return Ok(result);
        }
    }
}
