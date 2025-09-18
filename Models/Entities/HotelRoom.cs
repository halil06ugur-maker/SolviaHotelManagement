using System.Text.Json.Serialization;

namespace SolviaHotelManagement.Models.Entities
{
    public class HotelRoom
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        [JsonIgnore]
        public Hotel Hotel { get; set; }
        public int RoomId { get; set; }
        [JsonIgnore]
        public Room Room { get; set; }
        public string Type { get; set; }
        public bool IsReserved { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
