using AutoMapper;
using RestSharp;
using Trainin_App_for_Repository.CQRS.Request.Command;
using Trainin_App_for_Repository.CQRS.Request.Command.Address;
using Trainin_App_for_Repository.CQRS.Request.Command.Car;
using Trainin_App_for_Repository.CQRS.Request.Query.User;
using Trainin_App_for_Repository.CQRS.Response.Command;
using Trainin_App_for_Repository.CQRS.Response.Command.User;
using Trainin_App_for_Repository.CQRS.Response.Query;
using Trainin_App_for_Repository.CQRS.Response.Query.FavStation;
using Trainin_App_for_Repository.CQRS.Response.Query.User;
using Trainin_App_for_Repository.Data.DTO;
using Trainin_App_for_Repository.Data.DTO.Brand;
using Trainin_App_for_Repository.Data.Entities;

namespace Trainin_App_for_Repository.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UsersEntity, UserCreate>().ReverseMap();
            CreateMap<UsersEntity, UserUpdate>().ReverseMap();
            CreateMap<UsersEntity, UserDto>().ReverseMap();
            CreateMap<UserLogin, UsersEntity>().ReverseMap();
            CreateMap<UsersEntity, UserToken>().ReverseMap();
            CreateMap<UpdateUserCommand, UsersEntity>().ReverseMap();
            CreateMap<UserLoginQueryRequest, UsersEntity>().ReverseMap();

            CreateMap<AddressCreate, AddressesEntity>().ReverseMap();
            CreateMap<AddressesEntity, AddressDto>().ReverseMap();
            CreateMap<UpdateAddressCommand, AddressesEntity>().ReverseMap();

            CreateMap<FavStationResponse, FavStationsEntity>().ReverseMap();
            CreateMap<FavStationCreate, FavStationsEntity>().ReverseMap();

            CreateMap<CarCreate, CarsEntity>().ReverseMap();
            CreateMap<CarDto, CarsEntity>().ReverseMap();
            CreateMap<UpdateCarCommand, CarsEntity>().ReverseMap();

            CreateMap<BrandCreateDto, BrandsEntity>().ReverseMap();
            CreateMap<BrandUpdateDto, BrandsEntity>().ReverseMap();
            CreateMap<RestResponse, BrandGetLpgInfoDto>().ReverseMap();
            

        }
    }
}
