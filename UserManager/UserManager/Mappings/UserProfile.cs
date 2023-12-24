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
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
                //.ForMember(u => u.Id, opt => opt.Ignore()); // skip Id

            CreateMap<AddressDto, Address>();
            CreateMap<Address, AddressDto>();

            CreateMap<LocationDto, Location>();
            CreateMap<Location, LocationDto>();

            CreateMap<CompanyDto, Company>();
            CreateMap<Company, CompanyDto>();
        }
    }
}
