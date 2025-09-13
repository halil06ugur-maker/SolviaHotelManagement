namespace SolviaHotelManagement.Models.Entities
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TaxNumber { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<EmployeeHotelRole> EmployeeHotelRoles { get; set; } = new List<EmployeeHotelRole>();
        public ICollection<HotelAddress> HotelAddresses { get; set; } = new List<HotelAddress>();
        public HotelProperty? HotelProperty { get; set; }
        public ICollection<HotelImage> HotelImages { get; set; } = new List<HotelImage>();
        public ICollection<HotelRoom> HotelRooms { get; set; } = new List<HotelRoom>();
        public ICollection<CustomerHotelRoom> CustomerHotelRooms { get; set; } = new List<CustomerHotelRoom>();
        public ICollection<CustomerHotelRate> CustomerHotelRates { get; set; } = new List<CustomerHotelRate>();
    }
}
