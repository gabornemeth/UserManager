using UserManager.Contracts.Dtos;
using UserManager.Models;

namespace UserManager.Test.Mappings
{
    public class AddressMapTests : MapTests
    {
        [Fact]
        public void DtoWithEmptyLocation_NullLocationInModel()
        {
            var dto = new AddressDto();
            var model = Mapper.Map<Address>(dto);
            
            // assert
            model.GeoLocation.Should().BeNull();
        }

        [Fact]
        public void ModelWithNullLocation_NullLocationInDto()
        {
            var model = new Address { City = "Zalaegerszeg", ZipCode = "8900", Street = "Florian street 3" };
            var dto = Mapper.Map<AddressDto>(model);

            // assert
            dto.Geolocation.Should().BeNull();
        }
    }
}
