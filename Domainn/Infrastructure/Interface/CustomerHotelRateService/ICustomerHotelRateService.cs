using SolviaHotelManagement.Models.ServiceResult;
using SolviaHotelManagement.Models.ViewModels.CustomerHotelRate;

namespace SolviaHotelManagement.Domainn.Infrastructure.Interface.CustomerHotelRate
{
    public interface ICustomerHotelRateService
    {
        Task<ServiceResult> GetRateListAsync();
        Task<ServiceResult> GetRateByIdAsync(int id);
        Task<ServiceResult> AddRateAsync(CustomerHotelRateViewModel viewModel);
        Task<ServiceResult> UpdateRateAsync(CustomerHotelRateViewModel viewModel);
        Task<ServiceResult> DeleteRateAsync(int id);
    }
}
