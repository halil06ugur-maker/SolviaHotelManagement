namespace SolviaHotelManagement.Models.Entities
{
    public class HotelRoom
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public string Type { get; set; }
        public bool IsReserved { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
