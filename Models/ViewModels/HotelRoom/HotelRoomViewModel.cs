namespace SolviaHotelManagement.Models.ViewModels.HotelRoom
{
    public class HotelRoomViewModel
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public string Type { get; set; }
        public bool IsReserved { get; set; }
        public DateTime CreatedDate { get; set; }

        // İlişkiler (ileride açılabilir)
        // public HotelViewModel? Hotel { get; set; }
        // public RoomViewModel? Room { get; set; }
    }
}
