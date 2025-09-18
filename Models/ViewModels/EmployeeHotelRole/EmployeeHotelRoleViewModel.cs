namespace SolviaHotelManagement.Models.ViewModels.EmployeeHotelRole
{
    public class EmployeeHotelRoleViewModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int HotelId { get; set; }
        public int RoleId { get; set; }
        public DateTime CreatedDate { get; set; }

        // public EmployeeViewModel? Employee { get; set; }
        // public HotelViewModel? Hotel { get; set; }
        // public RoleViewModel? Role { get; set; }
    }
}
