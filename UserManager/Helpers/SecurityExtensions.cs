using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace UserManager
{
    public static class SecurityExtensions
    {
        public static AuthorizationPolicyBuilder HasScope(this AuthorizationPolicyBuilder policy, string scope)
        {
            return policy.RequireAssertion(ctx => ctx.User.Claims.HasScope(scope));
        }

        internal static bool HasScope(this IEnumerable<Claim> claims, string scope)
        {
            var scopeClaim = claims.FirstOrDefault(c => c.Type == "scope");
            if (scopeClaim == null)
                return false;

            return scopeClaim.Value?.Contains(scope) ?? false;
        }
    }
}
