using Microsoft.AspNetCore.JsonPatch;
using UserManager.Contracts.Dtos;
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

        public static void ApplyPatch(this UserDto user, JsonPatchDocument<UserDto> patch)
        {
            if (patch.Operations.Any(op => op.path.Contains("company", StringComparison.InvariantCultureIgnoreCase)))
            {
                if (user.Company == null)
                {
                    user.Company = new CompanyDto { Name = "" };
                }
            }

            patch.ApplyTo(user);
        }
    }
}
