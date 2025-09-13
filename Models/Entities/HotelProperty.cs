namespace SolviaHotelManagement.Models.Entities
{
    public class HotelProperty
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public int Capacity { get; set; }
        public bool IsShuttleTransfer { get; set; }
        public bool IsAnimalAccept { get; set; }
        public bool IsConcept { get; set; }
        public bool IsSpa { get; set; }
        public bool IsAdult { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
