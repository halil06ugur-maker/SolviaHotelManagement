namespace SolviaHotelManagement.Models.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<EmployeeHotelRole> EmployeeHotelRoles { get; set; } = new List<EmployeeHotelRole>();

    }
}
