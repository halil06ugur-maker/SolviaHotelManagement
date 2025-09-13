namespace SolviaHotelManagement.Models.Entities
{
    public class EmployeeHotelRole
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
