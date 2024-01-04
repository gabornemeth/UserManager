namespace UserManager.Test.Mappings
{
    public abstract class MapTests
    {
        protected IMapper Mapper { get; }

        public MapTests()
        {
            Mapper = TestHelper.CreateMapper();
        }
    }
}
