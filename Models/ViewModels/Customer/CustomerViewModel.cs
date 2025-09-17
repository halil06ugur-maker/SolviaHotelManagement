using SolviaHotelManagement.Models.ViewModels.CustomerHotelRate;
using SolviaHotelManagement.Models.ViewModels.CustomerHotelRoom;

namespace SolviaHotelManagement.Models.ViewModels.Customer
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string IdentityNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public DateTime CreatedDate { get; set; }

        // İlişkiler - şimdilik yorum satırında
        public List<CustomerHotelRoomViewModel>? CustomerHotelRooms { get; set; }
        public List<CustomerHotelRateViewModel>? CustomerHotelRates { get; set; }
    }
}
