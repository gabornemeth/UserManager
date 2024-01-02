using AutoMapper;
using MongoDB.Driver;
using Testcontainers.MongoDb;
using UserManager.Mappings;
using UserManager.Models;
using UserManager.Mongo;

namespace UserManager.Test.Services
{
    public class DbFixture : IAsyncLifetime
    {
        private MongoDbContainer? _container;

        public async Task DisposeAsync()
        {
            if (_container == null) return;
            await _container.StopAsync();
            await _container.DisposeAsync();
        }

        public async Task InitializeAsync()
        {
            _container = new MongoDbBuilder()
                .WithImage("mongo:6.0")
                .WithPortBinding(28018, 28018)
                .Build();
            await _container.StartAsync();
        }

        public string GetConnectionString() => _container?.GetConnectionString() ?? "";
    }

    public class MongoUserRepositoryTests : IClassFixture<DbFixture>
    {
        private MongoUserRepository _repository;
        private readonly DbFixture _fixture;

        public MongoUserRepositoryTests(DbFixture fixture)
        {
            _fixture = fixture;
            var mapper = new MapperConfiguration(config => config.AddProfile<UserProfile>()).CreateMapper();
            _repository = new MongoUserRepository(_fixture.GetConnectionString(), "users", mapper);
            _repository.Seed();
        }

        [Fact]
        public async Task VerifyInitialData()
        {
            var allUsers = await _repository.GetAll();
            var userToGet = allUsers.FirstOrDefault(u => u.Name == "Patricia Lebsack");
            Assert.NotNull(userToGet);

            var user = await _repository.Get(userToGet.Id);
            Assert.NotNull(user);

            user.Name.Should().Be("Patricia Lebsack");
            user.UserName.Should().Be("Karianne");
            user.Email.Should().Be("Julianne.OConner@kory.org");
            user.Phone.Should().Be("493-170-9623 x156");
            user.Website.Should().Be("kale.biz");

            Assert.NotNull(user.Address);
            user.Address.Street.Should().Be("Hoeger Mall");
            user.Address.Suite.Should().Be("Apt. 692");
            user.Address.City.Should().Be("South Elvis");
            user.Address.ZipCode.Should().Be("53919-4257");

            Assert.NotNull(user.Address.Geolocation);
            user.Address.Geolocation.Latitude.Should().Be(29.4572f);
            user.Address.Geolocation.Longitude.Should().Be(-164.2990f);

            Assert.NotNull(user.Company);
            user.Company.Name.Should().Be("Robel-Corkery");
            user.Company.CatchPhrase.Should().Be("Multi-tiered zero tolerance productivity");
            user.Company.BusinessServices.Should().Be("transition cutting-edge web services");
        }

        [Fact]
        public async Task Create()
        {
            var user = new User
            {
                Name = "Test User",
                UserName = "testuser"
            };
            await _repository.Create(user);

            var userCreated = await _repository.Get(user.Id);
            Assert.NotNull(userCreated);
            userCreated.Name.Should().Be(user.Name);
            userCreated.UserName.Should().Be(user.UserName);
        }

        [Fact]
        public async Task Delete()
        {
            var users = await _repository.GetAll();
            var userToDelete = users.FirstOrDefault(u => u.Name == "Nicholas Runolfsdottir V");
            Assert.NotNull(userToDelete);

            var deleteResult = await _repository.Delete(userToDelete);
            deleteResult.Should().BeTrue();

            users = await _repository.GetAll();
            users.Should().NotContain(u => u.Name == "Nicholas Runolfsdottir V");
        }

        [Fact]
        public async Task DeleteNonExisting()
        {
            var userToDelete = new User() { Name = "Non Existing User" };

            var deleteResult = await _repository.Delete(userToDelete!);
            deleteResult.Should().BeFalse();
        }

        [Fact]
        public async Task Update()
        {
            // arrange
            var users = await _repository.GetAll();
            var userToUpdate = users.FirstOrDefault(u => u.Name == "Leanne Graham");
            Assert.NotNull(userToUpdate);

            // act
            userToUpdate.UserName = $"{userToUpdate.UserName}.updated";
            var updateResult = await _repository.Update(userToUpdate);

            // assert
            updateResult.Should().BeTrue();
            users = await _repository.GetAll();
            var userUpdated = users.FirstOrDefault(u => u.Name == "Leanne Graham");
            Assert.NotNull(userUpdated);
            userUpdated.UserName.Should().Be(userToUpdate.UserName);
        }

        [Fact]
        public async Task UpdateNonExisting()
        {
            // arrange
            var userToUpdate = new User { Name = "Non existing user" };

            // act
            userToUpdate.UserName = $"{userToUpdate.UserName}.updated";
            var updateResult = await _repository.Update(userToUpdate!);

            // assert
            updateResult.Should().BeFalse();
            var users = await _repository.GetAll();
            var userUpdated = users.FirstOrDefault(u => u.Name == "Non existing user");
            Assert.Null(userUpdated);
        }

    }
}
