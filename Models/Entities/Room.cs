namespace SolviaHotelManagement.Models.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<HotelRoom> HotelRooms { get; set; } = new List<HotelRoom>();
        public ICollection<CustomerHotelRoom> CustomerHotelRooms { get; set; } = new List<CustomerHotelRoom>();
    }
}
