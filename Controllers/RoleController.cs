using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolviaHotelManagement.Domainn.Infrastructure.Interface.RoleService;
using SolviaHotelManagement.Models.ViewModels.Role;

namespace SolviaHotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }


        [HttpGet]
        public async Task<IActionResult> GetRoleListAsync()
        {
            var result = await _roleService.GetRoleListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleByIdAsync(int id)
        {
            var result = await _roleService.GetRoleByIdAsync(id);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> AddRoleAsync([FromBody] RoleViewModel viewModel)
        {
            var result = await _roleService.AddRoleAsync(viewModel);
            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoleAsync([FromBody] RoleViewModel viewModel)
        {
            var result = await _roleService.UpdateRoleAsync(viewModel);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoleAsync(int id)
        {
            var result = await _roleService.DeleteRoleAsync(id);
            return Ok(result);
        }
    }
}
