using SolviaHotelManagement.Models.ServiceResult;
using SolviaHotelManagement.Models.ViewModels.HotelAddress;

namespace SolviaHotelManagement.Domainn.Infrastructure.Interface.HotelAdressService
{
    public interface IHotelAddressService
    {
        Task<ServiceResult> GetByHotelIdAsync(int hotelId);
        Task<ServiceResult> GetListAsync();  // Tüm otel adreslerini getirir
        Task<ServiceResult> GetListByHotelIdAsync(int hotelId);  // Belirli otelin tüm adresleri
        Task<ServiceResult> AddHotelAddressAsync(HotelAddressViewModel viewModel);
        Task<ServiceResult> UpdateHotelAddressAsync(HotelAddressViewModel viewModel);
        Task<ServiceResult> DeleteHotelAddressAsync(int id);
    }
}
