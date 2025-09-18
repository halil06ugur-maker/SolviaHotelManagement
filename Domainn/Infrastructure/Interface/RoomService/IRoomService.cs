using SolviaHotelManagement.Models.ServiceResult;
using SolviaHotelManagement.Models.ViewModels.Room;

namespace SolviaHotelManagement.Domainn.Infrastructure.Interface.RoomService
{
    public interface IRoomService
    {
        Task<ServiceResult> GetRoomListAsync();
        Task<ServiceResult> GetRoomByIdAsync(int id);
        Task<ServiceResult> AddRoomAsync(RoomViewModel viewModel);
        Task<ServiceResult>UpdateRoomAsync(RoomViewModel viewModel);
       Task<ServiceResult> DeleteRoomAsync(int id);
    }
}
