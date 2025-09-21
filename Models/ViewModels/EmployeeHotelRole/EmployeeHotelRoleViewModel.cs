using SolviaHotelManagement.Models.ViewModels.Employee;
using SolviaHotelManagement.Models.ViewModels.Hotel;
using SolviaHotelManagement.Models.ViewModels.Role;
using System.Text.Json.Serialization;

namespace SolviaHotelManagement.Models.ViewModels.EmployeeHotelRole

{
    public class EmployeeHotelRoleViewModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        [JsonIgnore] public EmployeeViewModel? Employee { get; set; }
        public int HotelId { get; set; }
        [JsonIgnore] public HotelViewModel? Hotel { get; set; }
        public int RoleId { get; set; }
        [JsonIgnore] public RoleViewModel? Role { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
