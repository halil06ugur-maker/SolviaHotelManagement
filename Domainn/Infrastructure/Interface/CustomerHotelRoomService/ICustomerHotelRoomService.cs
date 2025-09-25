using SolviaHotelManagement.Models.ServiceResult;
using SolviaHotelManagement.Models.ViewModels.CustomerHotelRoom;

namespace SolviaHotelManagement.Domainn.Infrastructure.Interface.CustomerHotelRoomService
{
    public interface ICustomerHotelRoomService
    {
        Task<ServiceResult> GetReservationListAsync();
        Task<ServiceResult> GetReservationByIdAsync(int HotelId);
        Task<ServiceResult> AddReservationAsync(CustomerHotelRoomViewModel viewModel);
        Task<ServiceResult> UpdateReservationAsync(CustomerHotelRoomViewModel viewModel);
        Task<ServiceResult> DeleteReservationAsync(int id);
    }
}
