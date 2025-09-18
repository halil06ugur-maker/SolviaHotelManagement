namespace SolviaHotelManagement.Models.ViewModels.HotelImage
{
    public class HotelImageViewModel
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string ImgUrl { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
