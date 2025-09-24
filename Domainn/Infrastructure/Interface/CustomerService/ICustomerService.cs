using SolviaHotelManagement.Models.ServiceResult;
using SolviaHotelManagement.Models.ViewModels.Customer;

namespace SolviaHotelManagement.Domainn.Infrastructure.Interface.CustomerService
{
    public interface ICustomerService
    {
        Task<ServiceResult> GetCustomerListAsync();
        Task<ServiceResult> GetCustomerByIdAsync(int id);
        Task<ServiceResult> AddCustomerAsync(CustomerViewModel viewModel);
        Task<ServiceResult> UpdateCustomerAsync(CustomerViewModel viewModel);
        Task<ServiceResult> DeleteCustomerAsync(int id);
    }
}
