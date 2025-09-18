using SolviaHotelManagement.Models.ServiceResult;
using SolviaHotelManagement.Models.ViewModels.Role;

namespace SolviaHotelManagement.Domainn.Infrastructure.Interface.RoleService
{
    public interface IRoleService
    {
        Task<ServiceResult> GetRoleListAsync();
        Task<ServiceResult> GetRoleByIdAsync(int id);
        Task<ServiceResult> AddRoleAsync(RoleViewModel viewModel);
        Task<ServiceResult> UpdateRoleAsync(RoleViewModel viewModel);
        Task<ServiceResult> DeleteRoleAsync(int id);
    }
}
