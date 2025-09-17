namespace SolviaHotelManagement.Models.ViewModels.CustomerHotelRate
{
    public class CustomerHotelRateViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int HotelId { get; set; }
        public int Rate { get; set; }   // Örn: 1–5 arası puan
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }

        // İlişkiler - şimdilik yorumda bırak
        // public CustomerViewModel? Customer { get; set; }
        // public HotelViewModel? Hotel { get; set; }
    }
}
