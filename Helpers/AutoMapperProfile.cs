using AutoMapper;
using WebApi.Entities;
using WebApi.Models.City;
using WebApi.Models.Group;
using WebApi.Models.Item;
using WebApi.Models.Qualities;
using WebApi.Models.Stores;
using WebApi.Models.SubGroups;
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
            CreateMap<storeUpdateModel, Stores>();
            CreateMap<Stores, StoresModel>();

            CreateMap<RegisterGroupModel, Groups>();
            CreateMap<GroupUpdateModel, Groups>();
            CreateMap<Groups, GroupModel>();

            CreateMap<RegisterSubGroupModel, SubGroups>();
            CreateMap<subGroupUpdateModel, SubGroups>();
            CreateMap<SubGroups, SubGroupModel>();

            CreateMap<RegisterItemModel, Items>();
            CreateMap<ItemUpdateModel, Items>();
            CreateMap<Items, ItemModel>();

            CreateMap<RegisterQualitiesModel, Qualities>();
            CreateMap<QualitiesUpdateModel, Qualities>();
            CreateMap<Qualities, QualitiesModel>();
        }
    }
}