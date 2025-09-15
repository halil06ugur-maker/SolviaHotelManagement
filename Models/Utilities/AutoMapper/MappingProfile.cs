using AutoMapper;
using SolviaHotelManagement.Models.Entities;
using SolviaHotelManagement.Models.VİewModels.Hotel;
using SolviaHotelManagement.Models.VİewModels.HotelAdress;
using SolviaHotelManagement.Models.VİewModels.HotelProperty;

namespace SolviaHotelManagement.Models.Utilities.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Hotel, HotelViewModel>().ReverseMap();
            CreateMap<HotelAddress,HotelAddressViewModel>().ReverseMap();
            CreateMap<HotelProperty,HotelPropertyViewModel>().ReverseMap();

           
        }
    }
}
