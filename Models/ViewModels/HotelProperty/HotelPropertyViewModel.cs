namespace SolviaHotelManagement.Models.ViewModels.HotelProperty
{
    public class HotelPropertyViewModel
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int Capacity { get; set; }
        public bool IsShuttleTransfer { get; set; }
        public bool IsAnimalAccept { get; set; }
        public bool IsConcept { get; set; }
        public bool IsSpa { get; set; }
        public bool IsAdult { get; set; }
    }
}
