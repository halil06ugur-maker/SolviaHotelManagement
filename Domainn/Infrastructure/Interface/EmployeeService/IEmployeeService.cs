using SolviaHotelManagement.Models.ServiceResult;
using SolviaHotelManagement.Models.ViewModels.Employee;

namespace SolviaHotelManagement.Domainn.Infrastructure.Interface.EmployeeService
{
    public interface IEmployeeService
    {
        Task<ServiceResult> GetEmployeeListAsync();
        Task<ServiceResult> GetEmployeeByIdAsync(int id);
        Task<ServiceResult> AddEmployeeAsync(EmployeeViewModel viewModel);
        Task<ServiceResult> UpdateEmployeeAsync(EmployeeViewModel viewModel);
        Task<ServiceResult> DeleteEmployeeAsync(int id);
    }
}
