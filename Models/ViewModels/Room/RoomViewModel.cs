using SolviaHotelManagement.Models.ViewModels.CustomerHotelRoom;
using SolviaHotelManagement.Models.ViewModels.HotelRoom;

namespace SolviaHotelManagement.Models.ViewModels.Room
{
    public class RoomViewModel
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<HotelRoomViewModel>? HotelRooms { get; set; }
        //public List<CustomerHotelRoomViewModel>? CustomerHotelRooms { get; set; }
    }
}
    