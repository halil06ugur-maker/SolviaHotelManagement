using SolviaHotelManagement.Models.ServiceResult;
using SolviaHotelManagement.Models.ViewModels.HotelImage;

namespace SolviaHotelManagement.Domainn.Infrastructure.Interface.HotelImageService
{
    public interface IHotelImageService
    {
        // Belirli bir otel resmini ID ile getirir
        Task<ServiceResult> GetByHotelImageIdAsync(int hotelImageId);

        // Tüm otel resimlerini listeler
        Task<ServiceResult> GetListAsync();

        // Belirli bir otele ait tüm resimleri listeler
        Task<ServiceResult> GetListByHotelIdAsync(int hotelId);

        // Yeni bir otel resmi ekler
        Task<ServiceResult> AddHotelImageAsync(HotelImageViewModel viewModel);

        // Bir otel resmini günceller
        Task<ServiceResult> UpdateHotelImageAsync(HotelImageViewModel viewModel);

        // Bir otel resmini siler
        Task<ServiceResult> DeleteHotelImageAsync(int id);
    }
}
