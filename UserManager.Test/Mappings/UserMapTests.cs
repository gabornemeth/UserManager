using UserManager.Contracts.Dtos;
using UserManager.Models;

namespace UserManager.Test.Mappings
{
    public class UserMapTests : MapTests
    {
        [Fact]
        public void DtoWithEmptyCompany_NullCompanyInModel()
        {
            var dto = new UserDto() { Company = new CompanyDto() };
            var model = Mapper.Map<User>(dto);
            
            // assert
            model.Company.Should().BeNull();
        }

        [Fact]
        public void ModelWithNullCompany_NullCompanyInDto()
        {
            var model = new User();
            var dto = Mapper.Map<UserDto>(model);

            // assert
            dto.Company.Should().BeNull();
        }

        [Fact]
        public void DtoWithAddress_NullAddressInModel()
        {
            var dto = new UserDto() { Address = new AddressDto() };
            var model = Mapper.Map<User>(dto);

            // assert
            model.Address.Should().BeNull();
        }

        [Fact]
        public void ModelWithNullAddress_NullAddressInDto()
        {
            var model = new User();
            var dto = Mapper.Map<UserDto>(model);

            // assert
            dto.Address.Should().BeNull();
        }
    }
}
