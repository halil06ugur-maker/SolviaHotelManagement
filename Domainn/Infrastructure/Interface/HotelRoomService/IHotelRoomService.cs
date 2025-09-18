using SolviaHotelManagement.Models.ServiceResult;
using SolviaHotelManagement.Models.ViewModels.HotelRoom;

namespace SolviaHotelManagement.Domainn.Infrastructure.Interface.HotelRoomService
{
    public interface IHotelRoomService
    {
        // Belirli bir otel odasını ID ile getirir
        Task<ServiceResult> GetByIdAsync(int id);

        // Tüm otel-oda eşleşmelerini listeler
        Task<ServiceResult> GetListAsync();

        // Belirli bir otele ait tüm odaları listeler
        Task<ServiceResult> GetListByHotelIdAsync(int hotelId);

        // Belirli bir oda ID’sine göre tüm otel-oda kayıtlarını getirir
        Task<ServiceResult> GetListByRoomIdAsync(int roomId);

        // Yeni bir otel odası kaydı (ilişkilendirme) ekler
        Task<ServiceResult> AddAsync(HotelRoomViewModel viewModel);

        // Mevcut bir otel odası kaydını günceller
        Task<ServiceResult> UpdateAsync(HotelRoomViewModel viewModel);

        // Otel odası kaydını siler
        Task<ServiceResult> DeleteAsync(int id);
    }
}
