using SolviaHotelManagement.Models.ServiceResult;
using SolviaHotelManagement.Models.ViewModels.Hotel;

namespace SolviaHotelManagement.Domainn.Infrastructure.Interface.HotelService
{
    public interface IHotelService
    {
        //Listeleme
        Task<ServiceResult> GetHotelListAsync();
        //Id'ye göre listeleme
        Task<ServiceResult> GetHotelByIdAsync(int id);
        //Ekleme
        Task<ServiceResult> AddHotelAsync(HotelViewModel ViewModel);
        //Güncelleme
        Task<ServiceResult> UpdateHotelAsync(HotelViewModel ViewModel);
        //Silme
        Task<ServiceResult> DeleteHotelAsync(int id);
    }
}
