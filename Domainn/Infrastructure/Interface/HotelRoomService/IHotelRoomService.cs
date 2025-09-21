using SolviaHotelManagement.Models.ServiceResult;
using SolviaHotelManagement.Models.ViewModels.HotelRoom;

namespace SolviaHotelManagement.Domainn.Infrastructure.Interface.HotelRoomService
{
    public interface IHotelRoomService
    {
        Task<ServiceResult> GetAllHotelRooms();
        Task<ServiceResult> GetListByHotelIdAsync(int hotelId);
        Task<ServiceResult> GetHotelRoomByIdAsync(int id);
        Task<ServiceResult> CreateHotelRoomAsync(HotelRoomViewModel model);
        Task<ServiceResult> UpdateHotelRoomAsync(HotelRoomViewModel model);
        Task<ServiceResult> DeleteHotelRoomAsync(int id);
    }
}
