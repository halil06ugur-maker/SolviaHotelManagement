namespace SolviaHotelManagement.Models.Entities
{
    public class CustomerHotelRoom
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
