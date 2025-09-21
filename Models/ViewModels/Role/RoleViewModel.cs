using SolviaHotelManagement.Models.ViewModels.EmployeeHotelRole;

namespace SolviaHotelManagement.Models.ViewModels.Role
{
    public class RoleViewModel
    {
        public int Id { get; set; }  
        public string Name { get; set; } 
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<EmployeeHotelRoleViewModel>? EmployeeHotelRoles { get; set; }
    }
}
