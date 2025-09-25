using SolviaHotelManagement.Models.ViewModels.Customer;
using SolviaHotelManagement.Models.ViewModels.Hotel;
using SolviaHotelManagement.Models.ViewModels.Room;
using System.Text.Json.Serialization;

namespace SolviaHotelManagement.Models.ViewModels.CustomerHotelRoom
{
    public class CustomerHotelRoomViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        [JsonIgnore]
        public CustomerViewModel? Customer { get; set; }
        public int HotelId { get; set; }
        [JsonIgnore]
        public HotelViewModel? Hotel { get; set; }
        public int RoomId { get; set; }
        [JsonIgnore]
        public RoomViewModel? Room { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
