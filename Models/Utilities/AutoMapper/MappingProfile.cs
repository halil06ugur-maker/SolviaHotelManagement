using AutoMapper;
using SolviaHotelManagement.Models.Entities;
using SolviaHotelManagement.Models.ViewModels.Customer;
using SolviaHotelManagement.Models.ViewModels.CustomerHotelRate;
using SolviaHotelManagement.Models.ViewModels.CustomerHotelRoom;
using SolviaHotelManagement.Models.ViewModels.Employee;
using SolviaHotelManagement.Models.ViewModels.EmployeeHotelRole;
using SolviaHotelManagement.Models.ViewModels.Hotel;
using SolviaHotelManagement.Models.ViewModels.HotelAddress;
using SolviaHotelManagement.Models.ViewModels.HotelImage;
using SolviaHotelManagement.Models.ViewModels.HotelProperty;
using SolviaHotelManagement.Models.ViewModels.HotelRoom;
using SolviaHotelManagement.Models.ViewModels.Role;
using SolviaHotelManagement.Models.ViewModels.Room;

namespace SolviaHotelManagement.Models.Utilities.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Room, RoomViewModel>().ReverseMap();

            CreateMap<Hotel, HotelViewModel>().ReverseMap();
            CreateMap<HotelAddress, HotelAddressViewModel>().ReverseMap();
            CreateMap<HotelProperty, HotelPropertyViewModel>().ReverseMap();
            CreateMap<HotelImageViewModel, HotelImage>().ReverseMap();
            CreateMap<HotelRoom, HotelRoomViewModel>().ReverseMap();

            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
            CreateMap<Role, RoleViewModel>().ReverseMap();
            CreateMap<EmployeeHotelRole, EmployeeHotelRoleViewModel>().ReverseMap();


            CreateMap<Customer, CustomerViewModel>().ReverseMap();
            CreateMap<CustomerHotelRoom, CustomerHotelRoomViewModel>().ReverseMap();
            CreateMap<CustomerHotelRate, CustomerHotelRateViewModel>().ReverseMap();



        }
    }
}
