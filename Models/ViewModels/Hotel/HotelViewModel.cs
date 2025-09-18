using SolviaHotelManagement.Models.ViewModels.HotelAddress;
using SolviaHotelManagement.Models.ViewModels.HotelImage;
using SolviaHotelManagement.Models.ViewModels.HotelProperty;
using SolviaHotelManagement.Models.ViewModels.HotelRoom;

namespace SolviaHotelManagement.Models.ViewModels.Hotel
{
    public class HotelViewModel
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string taxNumber { get; set; }
        public string phoneNumber { get; set; }
        public DateTime createdDate { get; set; }
        public List<HotelAddressViewModel>? hotelAddresses { get; set; }
        public HotelPropertyViewModel? HotelProperty { get; set; }
        public ICollection<HotelImageViewModel>? HotelImages { get; set; }
        public ICollection<HotelRoomViewModel>? HotelRooms { get; set; }
    }
}
