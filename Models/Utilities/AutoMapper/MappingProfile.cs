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

            CreateMap<Hotel, HotelViewModel>().ReverseMap();
            CreateMap<HotelAddress, HotelAddressViewModel>().ReverseMap();
            CreateMap<HotelProperty, HotelPropertyViewModel>().ReverseMap();
            CreateMap<HotelImageViewModel, HotelImage>().ReverseMap();
            CreateMap<Room, RoomViewModel>().ReverseMap();
            //CreateMap<HotelRoom, HotelRoomViewModel>().ReverseMap();

            CreateMap<HotelRoomViewModel, HotelRoom>()
            .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.RoomId))
            .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.HotelId))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.IsReserved, opt => opt.MapFrom(src => src.IsReserved))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate));

            // HotelRoom -> HotelRoomViewModel eşlemesi
            CreateMap<HotelRoom, HotelRoomViewModel>()
                .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.RoomId))
                .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.HotelId))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.IsReserved, opt => opt.MapFrom(src => src.IsReserved))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate));




            CreateMap<Customer, CustomerViewModel>().ReverseMap();
            CreateMap<CustomerHotelRoom, CustomerHotelRoomViewModel>().ReverseMap();
            CreateMap<CustomerHotelRate, CustomerHotelRateViewModel>().ReverseMap();
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
            CreateMap<Role, RoleViewModel>().ReverseMap();
            CreateMap<EmployeeHotelRole, EmployeeHotelRoleViewModel>().ReverseMap();

        }
    }
}
