using SolviaHotelManagement.Models.ViewModels.EmployeeHotelRole;

namespace SolviaHotelManagement.Models.ViewModels.Employee
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<EmployeeHotelRoleViewModel>? EmployeeHotelRoles { get; set; }
    }
}
