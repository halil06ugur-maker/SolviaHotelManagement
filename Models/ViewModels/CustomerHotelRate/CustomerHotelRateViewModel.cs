using SolviaHotelManagement.Models.ViewModels.Customer;
using SolviaHotelManagement.Models.ViewModels.Hotel;
using System.Text.Json.Serialization;

namespace SolviaHotelManagement.Models.ViewModels.CustomerHotelRate
{
    public class CustomerHotelRateViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int HotelId { get; set; }
        [JsonIgnore] public HotelViewModel? Hotel { get; set; }  // Hotel ile ilişki (Navigation Property)
        [JsonIgnore] public CustomerViewModel? Customer { get; set; }   // Customer ile ilişki (Navigation Property)
        public int Rate { get; set; }   // Örn: 1–5 arası puan
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        
    }
}
