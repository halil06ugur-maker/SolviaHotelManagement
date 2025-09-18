using SolviaHotelManagement.Models.ServiceResult;
using SolviaHotelManagement.Models.ViewModels.EmployeeHotelRole;

namespace SolviaHotelManagement.Domainn.Infrastructure.Interface.EmployeeHotelRoleService
{
    public interface IEmployeeHotelRoleService
    {
        Task<ServiceResult> GetEmployeeHotelRoleListAsync();
        Task<ServiceResult> GetEmployeeHotelRoleByIdAsync(int id);
        Task<ServiceResult> AddEmployeeHotelRoleAsync(EmployeeHotelRoleViewModel viewModel);
        Task<ServiceResult> UpdateEmployeeHotelRoleAsync(EmployeeHotelRoleViewModel viewModel);
        Task<ServiceResult> DeleteEmployeeHotelRoleAsync(int id);
    }
}
