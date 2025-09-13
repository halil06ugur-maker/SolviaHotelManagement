namespace SolviaHotelManagement.Models.Entities
{
    public class HotelImage
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public string ImgUrl { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
