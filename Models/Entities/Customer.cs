namespace SolviaHotelManagement.Models.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string IdentityNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<CustomerHotelRoom> CustomerHotelRooms { get; set; } = new List<CustomerHotelRoom>();
        public ICollection<CustomerHotelRate> CustomerHotelRates { get; set; } = new List<CustomerHotelRate>();

    }
}
