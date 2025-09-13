namespace SolviaHotelManagement.Models.Entities
{
    public class CustomerHotelRate
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public int Rate { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
