using AutoMapper;
using UserManager.Mappings;

namespace UserManager.Test.Mappings
{
    public abstract class MapTests
    {
        protected IMapper Mapper { get; }

        public MapTests()
        {
            Mapper = new MapperConfiguration(config => config.AddProfile<UserProfile>()).CreateMapper();
        }
    }
}
