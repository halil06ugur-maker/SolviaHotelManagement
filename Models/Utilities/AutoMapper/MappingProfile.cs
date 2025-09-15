using AutoMapper;
using SolviaHotelManagement.Models.Entities;
using SolviaHotelManagement.Models.VİewModels.Hotel;

namespace SolviaHotelManagement.Models.Utilities.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Hotel, HotelViewModel>().ReverseMap();

           
        }
    }
}
