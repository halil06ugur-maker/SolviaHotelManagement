using SolviaHotelManagement.Models.ServiceResult;
using SolviaHotelManagement.Models.ViewModels.HotelProperty;

namespace SolviaHotelManagement.Domainn.Infrastructure.Interface.HotelProperty
{
    public interface IHotelPropertyService
    {
        // Belirli bir otel özelliğini ID ile getirir
        Task<ServiceResult> GetByHotelPropertyIdAsync(int hotelPropertyId);

        // Tüm otel özelliklerini listeler
        Task<ServiceResult> GetListAsync();

        // Belirli bir otele ait tüm özellikleri listeler
        Task<ServiceResult> GetListByHotelIdAsync(int hotelId);

        // Yeni bir otel özelliği ekler
        Task<ServiceResult> AddHotelPropertyAsync(HotelPropertyViewModel viewModel);

        // Bir otel özelliğini günceller
        Task<ServiceResult> UpdateHotelPropertyAsync(HotelPropertyViewModel viewModel);

        // Bir otel özelliğini siler
        Task<ServiceResult> DeleteHotelPropertyAsync(int id);
    }
}
