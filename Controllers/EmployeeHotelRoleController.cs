using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolviaHotelManagement.Domainn.Infrastructure.Interface.EmployeeHotelRoleService;
using SolviaHotelManagement.Models.ViewModels.EmployeeHotelRole;

namespace SolviaHotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeHotelRoleController : ControllerBase
    {
        private readonly IEmployeeHotelRoleService _employeeHotelRoleService;

        public EmployeeHotelRoleController(IEmployeeHotelRoleService employeeHotelRoleService)
        {
            _employeeHotelRoleService = employeeHotelRoleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeHotelRoleListAsync()
        {
            var result = await _employeeHotelRoleService.GetEmployeeHotelRoleListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeHotelRoleByIdAsync(int id)
        {
            var result = await _employeeHotelRoleService.GetEmployeeHotelRoleByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeHotelRoleAsync([FromBody] EmployeeHotelRoleViewModel viewModel)
        {
            var result = await _employeeHotelRoleService.AddEmployeeHotelRoleAsync(viewModel);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeeHotelRoleAsync([FromBody] EmployeeHotelRoleViewModel viewModel)
        {
            var result = await _employeeHotelRoleService.UpdateEmployeeHotelRoleAsync(viewModel);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeHotelRoleAsync(int id)
        {
            var result = await _employeeHotelRoleService.DeleteEmployeeHotelRoleAsync(id);
            return Ok(result);
        }
    }
}
