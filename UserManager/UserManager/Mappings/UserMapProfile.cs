using AutoMapper;

namespace UserManager.Mappings
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<Mongo.User, Dtos.User>();
        }
    }
}
