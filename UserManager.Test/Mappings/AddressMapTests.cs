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
            model.Geolocation.Should().BeNull();
        }

        [Fact]
        public void DtoWithLocation_LocationInModel()
        {
            var dto = new AddressDto
            {
                City = "Zalaegerszeg",
                ZipCode = "8900",
                Street = "Florian street 3",
                Geolocation = new LocationDto
                {
                    Latitude = 46.839361f,
                    Longitude = 16.845722f
                }
            };

            var model = Mapper.Map<Address>(dto);

            // assert
            Assert.NotNull(model.Geolocation);
            model.Geolocation.Latitude.Should().Be(46.839361f);
            model.Geolocation.Longitude.Should().Be(16.845722f);
        }

        [Fact]
        public void DtoWithPartialLocation_LocationInModel()
        {
            var dto = new AddressDto
            {
                City = "Zalaegerszeg",
                ZipCode = "8900",
                Street = "Florian street 3",
                Geolocation = new LocationDto
                {
                    Longitude = 16.845722f
                }
            };

            var model = Mapper.Map<Address>(dto);

            // assert
            Assert.NotNull(model.Geolocation);
            model.Geolocation.Longitude.Should().Be(16.845722f);
        }

        [Fact]
        public void ModelWithNullLocation_NullLocationInDto()
        {
            var model = new Address { City = "Zalaegerszeg", ZipCode = "8900", Street = "Florian street 3" };
            var dto = Mapper.Map<AddressDto>(model);

            // assert
            dto.Geolocation.Should().BeNull();
        }

        [Fact]
        public void ModelWithLocation_LocationInDto()
        {
            var model = new Address
            {
                City = "Zalaegerszeg",
                ZipCode = "8900",
                Street = "Florian street 3",
                Geolocation = new Location(46.839361f, 16.845722f)
            };
            var dto = Mapper.Map<AddressDto>(model);

            // assert
            Assert.NotNull(dto.Geolocation);
            dto.Geolocation.IsEmpty().Should().Be(false);
            dto.Geolocation.Latitude.Should().Be(46.839361f);
            dto.Geolocation.Longitude.Should().Be(16.845722f);
        }
    }
}
