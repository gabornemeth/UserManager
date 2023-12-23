using AutoMapper;
using UserManager.Dtos;
using MongoUser = UserManager.Mongo.User;
using MongoCompany = UserManager.Mongo.Company;
using MongoAddress = UserManager.Mongo.Address;

namespace UserManager.Mappings
{
    /// <summary>
    /// AutoMapper configuration between model entities and MongoDB abstractions
    /// </summary>
    public class MongoDbProfile : Profile
    {
        public MongoDbProfile()
        {
            CreateMap<MongoUser, User>();
            CreateMap<User, MongoUser>();
                //.ForMember(u => u.Id, opt => opt.Ignore()); // skip Id

            CreateMap<Address, MongoAddress>();
            CreateMap<MongoAddress, Address>();

            CreateMap<Company, MongoCompany>();
            CreateMap<MongoCompany, Company>();
        }
    }
}
