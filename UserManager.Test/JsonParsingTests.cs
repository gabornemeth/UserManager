using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using UserManager.Contracts.Dtos;
using UserManager.Contracts.Requests;

namespace UserManager.Test
{
    public class JsonParsingTests
    {
        [Fact]
        public void JsonPatchDocument_ToJson()
        {
            var patch = new JsonPatchDocument<UserDto>();
            patch.Replace(u => u.Name, "Updated User");
            var json = JsonConvert.SerializeObject(patch);
        }

        [Fact]
        public void PartialUpdateUserRequest_ReplaceName_FromJson()
        {
            var user = ApplyPatch(new UserDto(),
                """{ update: [ { "op": "replace", "path": "/name", "value": "Updated User" } ] }""");

            user.Name.Should().Be("Updated User");
        }

        [Fact]
        public void PartialUpdateUserRequest_ReplaceEmail_FromJson()
        {
            var user = ApplyPatch(new UserDto(), """{ update: [ { "op": "replace", "path": "/email", "value": "new_user@old_domain.com" } ] }""");

            // assert
            user.Email.Should().Be("new_user@old_domain.com");
        }

        [Fact]
        public void PartialUpdateUserRequest_AddProperty_FromJson()
        {
            var user = ApplyPatch(new UserDto(), """{ update: [ { "op": "add", "path": "/website", "value": "www.mypage.com" } ] }""");

            // assert
            user.Website.Should().Be("www.mypage.com");
        }

        [Fact]
        public void PartialUpdateUserRequest_ReplaceCompanyName_FromJson()
        {
            var user = ApplyPatch(new UserDto { Company = new CompanyDto() },
                """{ update: [ { op: "replace", path: "/company/name", value: "Microsoft" } ] }""");

            // assert
            user.Company.Should().NotBeNull();
            user.Company!.Name.Should().Be("Microsoft");
        }

        [Fact]
        public void PartialUpdateUserRequest_ReplaceCompany_FromJson()
        {
            var user = ApplyPatch(new UserDto(),
                """{ update: [ { op: "replace", "path": "/company", "value": { "name": "Microsoft", "catchPhrase": "M$", "bs": "software development" } } ] }""");

            Assert.NotNull(user.Company);
            user.Company.Name.Should().Be("Microsoft");
            user.Company.CatchPhrase.Should().Be("M$");
            user.Company.BusinessServices.Should().Be("software development");
        }

        [Fact]
        public void PartialUpdateUserRequest_RemoveCompany_FromJson()
        {
            var user = ApplyPatch(new UserDto
            {
                Company = new CompanyDto
                {
                    Name = "Microsoft",
                    CatchPhrase = "M$",
                    BusinessServices = "software development"
                }
            },
            """{ "update": [ { "op": "remove", "path": "/company" } ] }""");

            // assert
            user.Company.Should().BeNull();
        }

        [Fact]
        public void PartialUpdateUserRequest_AddAddress()
        {
            var user = ApplyPatch(new UserDto(),
                """{ update: [ { op: "add", "path": "/address", "value": { "city": "Budapest", "zipCode": "1063" } } ] }""");

            user.Address.Should().NotBeNull();
            user.Address!.City.Should().Be("Budapest");
            user.Address!.ZipCode.Should().Be("1063");
        }

        [Fact]
        public void PartialUpdateUserRequest_ReplaceAddress()
        {
            var user = ApplyPatch(new UserDto(),
                """{ update: [ { op: "replace", "path": "/address", "value": { "city": "Budapest", "zipCode": "1063" } } ] }""");

            user.Address.Should().NotBeNull();
            user.Address!.City.Should().Be("Budapest");
            user.Address!.ZipCode.Should().Be("1063");
        }

        [Fact]
        public void PartialUpdateUserRequest_AddAddressWithGeolocation()
        {
            var user = ApplyPatch(new UserDto(),
                """
                { 
                    update:
                    [
                        {
                            "op": "add", "path": "/address", 
                            "value":
                            {
                                "city": "Zalaegerszeg",
                                "zipCode": 8900,
                                "street": "Kossuth u. 39",
                                "suite": "ground",
                                "geo": { "lat": "46.839361", "lng": "16.845722" } 
                            } 
                        }
                    ]
                }
                """);

            user.Address.Should().NotBeNull();
            user.Address!.City.Should().Be("Zalaegerszeg");
            user.Address!.ZipCode.Should().Be("8900");
            user.Address!.Street.Should().Be("Kossuth u. 39");
            user.Address!.Suite.Should().Be("ground");
            user.Address?.Geolocation.Should().NotBeNull();
            user.Address!.Geolocation!.Latitude.Should().Be(46.839361f);
            user.Address!.Geolocation!.Longitude.Should().Be(16.845722f);
        }

        [Fact]
        public void PartialUpdateUserRequest_AddAddressGeolocation()
        {
            var user = ApplyPatch(new UserDto { Address = new AddressDto() },
                """{ update: [ { op: "add", "path": "/address/geo", "value": { "lat": "46.839361", "lng": "16.845722" } } ] }""");

            user.Address?.Geolocation.Should().NotBeNull();
            user.Address!.Geolocation!.Latitude.Should().Be(46.839361f);
            user.Address!.Geolocation!.Longitude.Should().Be(16.845722f);
        }

        [Fact]
        public void PartialUpdateUserRequest_ReplaceAddressGeolocation()
        {
            var user = ApplyPatch(new UserDto { Address = new AddressDto() { Geolocation = new LocationDto() } },
                """{ update: [ { op: "replace", "path": "/address/geo/lat", "value": "46.839361" } ] }""");

            user.Address!.Geolocation!.Latitude.Should().Be(46.839361f);
        }

        private UserDto ApplyPatch(UserDto user, string jsonPatch)
        {
            var patch = JsonConvert.DeserializeObject<UpdateUserRequest>(jsonPatch);
            patch.Should().NotBeNull();
            patch!.Update.ApplyTo(user);

            return user;
        }
    }
}
