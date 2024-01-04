using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using UserManager.Services;

namespace UserManager.Test
{
    public class ExtensionsTests
    {
        [Fact]
        public void SeedDataCalledOnRepository()
        {
            var repository = new Mock<IUserRepository>();
            var builder = WebApplication.CreateBuilder();
            builder.Services.AddSingleton<IUserRepository>(_ => repository.Object);
            var app = builder.Build();
            app.SeedData();

            repository.Verify(r => r.Seed(), Times.Once());
        }
    }
}
