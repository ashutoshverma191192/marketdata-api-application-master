using AutoMapper;
using WebApi.Entities;
using WebApi.Models.City;
using WebApi.Models.Stores;
using WebApi.Models.Users;

namespace WebApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Users, UserModel>();
            CreateMap<RegisterModel, Users>();
            CreateMap<UpdateModel, Users>();
            
            CreateMap<RegisterCityModel, CityMasters>();
            CreateMap<CityMasters, CityModel>();

            CreateMap<RegisterStoreModel, Stores>();
        }
    }
}