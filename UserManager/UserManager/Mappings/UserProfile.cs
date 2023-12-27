using AutoMapper;
using UserManager.Contracts.Dtos;
using UserManager.Models;

namespace UserManager.Mappings
{
    /// <summary>
    /// AutoMapper configuration between model entities and MongoDB abstractions
    /// </summary>
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>();
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<AddressDto, Address>().ReverseMap();
            CreateMap<LocationDto, Location>().ReverseMap();
            CreateMap<CompanyDto, Company>().ReverseMap();
        }
    }
}
