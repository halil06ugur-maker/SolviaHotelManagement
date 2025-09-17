namespace SolviaHotelManagement.Models.ViewModels.HotelAddress
{
    public class HotelAddressViewModel
    {
        public int Id { get; set; }
        public int hotelId { get; set; }
        public string address { get; set; }
        public string postCode { get; set; }
        public DateTime createdDate { get; set; }

       
    }
}
