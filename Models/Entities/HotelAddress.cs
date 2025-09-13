namespace SolviaHotelManagement.Models.Entities
{
    public class HotelAddress
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<EmployeeHotelRole> EmployeeHotelRoles { get; set; } = new List<EmployeeHotelRole>();
        public ICollection<HotelAddress> HotelAddresses { get; set; } = new List<HotelAddress>();
    }
}
