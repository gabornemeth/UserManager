using UserManager.Contracts.Dtos;
using UserManager.Models;

namespace UserManager.Test.Mappings
{
    public class UserMapTestsWithUserDto : UserMapTests<UserDto>
    {
    }
    public class UserMapTestsWithUserDtoBase : UserMapTests<UserDtoBase>
    {
    }

    public abstract class UserMapTests<TUserDto> : MapTests where TUserDto : UserDtoBase, new()
    {
        [Fact]
        public void DtoWithEmptyCompany_NullCompanyInModel()
        {
            var dto = new TUserDto() { Company = new CompanyDto() };
            var model = Mapper.Map<User>(dto);

            // assert
            model.Company.Should().BeNull();
        }

        [Fact]
        public void ModelWithNullCompany_NullCompanyInDto()
        {
            var model = new User();
            var dto = Mapper.Map<TUserDto>(model);

            // assert
            dto.Company.Should().BeNull();
        }

        [Fact]
        public void DtoWithEmptyAddress_NullAddressInModel()
        {
            ShouldNotMapAddress(new TUserDto() { Address = new AddressDto() });
            ShouldNotMapAddress(new TUserDto() { Address = new AddressDto { Geolocation = new LocationDto() } });

            void ShouldNotMapAddress(TUserDto dto)
            {
                var model = Mapper.Map<User>(dto);

                // assert
                model.Address.Should().BeNull();
            }
        }

        [Fact]
        public void DtoWithNonEmptyAddress_AddressInModel()
        {
            ShouldMapAddress(new TUserDto() { Address = new AddressDto { City = "Zalaegerszeg" } });
            ShouldMapAddress(new TUserDto() { Address = new AddressDto { ZipCode = "8900" } });
            ShouldMapAddress(new TUserDto() { Address = new AddressDto { Street = "Kossuth u. 39" } });
            ShouldMapAddress(new TUserDto() { Address = new AddressDto { Suite = "fsz. 1" } });
            ShouldMapAddress(new TUserDto() { Address = new AddressDto { Geolocation = new LocationDto(46.839361f, 16.845722f) } });

            void ShouldMapAddress(TUserDto dto)
            {
                var model = Mapper.Map<User>(dto);

                // assert
                model.Address.Should().NotBeNull();
            }
        }


        [Fact]
        public void ModelWithNullAddress_NullAddressInDto()
        {
            var model = new User();
            var dto = Mapper.Map<TUserDto>(model);

            // assert
            dto.Address.Should().BeNull();
        }
    }
}
