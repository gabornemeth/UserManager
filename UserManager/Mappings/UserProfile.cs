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
            CreateMap<LocationDto, Location>().ReverseMap();
            CreateMap<AddressDto, Address>()
                .ForMember(a => a.GeoLocation, opt => opt.Condition(a => !a.Geolocation?.IsEmpty() ?? true));
            CreateMap<Address, AddressDto>();

            CreateMap<CompanyDto, Company>().ReverseMap();

            CreateMap<CreateUserDto, User>()
                .ForMember(u => u.Company, opt => opt.Condition(u => !u.Company?.IsEmpty() ?? true))
                .ForMember(u => u.Address, opt => opt.Condition(u => !u.Address?.IsEmpty() ?? true));
            CreateMap<UserDto, User>()
                .ForMember(u => u.Company, opt => opt.Condition(u => !u.Company?.IsEmpty() ?? true))
                .ForMember(u => u.Address, opt => opt.Condition(u => !u.Address?.IsEmpty() ?? true));

            CreateMap<User, UserDto>();
        }
    }
}
