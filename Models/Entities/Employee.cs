namespace SolviaHotelManagement.Models.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<EmployeeHotelRole> EmployeeHotelRoles { get; set; } = new List<EmployeeHotelRole>();
    }
}
