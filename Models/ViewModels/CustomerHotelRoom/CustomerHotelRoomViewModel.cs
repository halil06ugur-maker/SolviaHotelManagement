namespace SolviaHotelManagement.Models.ViewModels.CustomerHotelRoom
{
    public class CustomerHotelRoomViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }

        // İlişkiler (ileride açılabilir)
        //public CustomerViewModel? Customer { get; set; }
        // public HotelViewModel? Hotel { get; set; }
        // public RoomViewModel? Room { get; set; }
    }
}
