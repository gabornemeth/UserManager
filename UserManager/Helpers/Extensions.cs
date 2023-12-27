using UserManager.Services;

namespace UserManager.Helpers
{
    internal static class Extensions
    {
        public static void SeedData(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var repo = scope.ServiceProvider.GetService<IUserRepository>();
            repo?.Seed();
        }
    }
}
