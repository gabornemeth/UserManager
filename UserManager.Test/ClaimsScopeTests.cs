using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UserManager.Test
{
    /// <summary>
    /// Tests for verifying included scopes in the claims
    /// </summary>
    public class ClaimsScopeTests
    {
        public static IEnumerable<object[]> GetTestData
        {
            get
            {
                var noScopeClaims = new[] {
                    new Claim("user", "gabor"),
                    new Claim("iat", "1704304355"),
                    new Claim("exp", "1704390755")
                };
                var scopeClaimWithoutPermissions = new[] {
                    new Claim("user", "gabor"),
                    new Claim("iat", "1704304355"),
                    new Claim("exp", "1704390755"),
                    new Claim("scope", "profile openid")
                };
                var scopeClaimWithReadPermission = new[] {
                    new Claim("user", "gabor"),
                    new Claim("iat", "1704304355"),
                    new Claim("exp", "1704390755"),
                    new Claim("scope", $"profile openid {Scopes.Read}")
                };
                var scopeClaimWithWritePermission = new[] {
                    new Claim("user", "gabor"),
                    new Claim("iat", "1704304355"),
                    new Claim("exp", "1704390755"),
                    new Claim("scope", $"profile openid {Scopes.Write}")
                };
                var scopeClaimWithReadAndWritePermissions = new[] {
                    new Claim("user", "gabor"),
                    new Claim("iat", "1704304355"),
                    new Claim("exp", "1704390755"),
                    new Claim("scope", $"profile openid {Scopes.Read} {Scopes.Write}")
                };
                var emptyScopeClaim = new[] {
                    new Claim("user", "gabor"),
                    new Claim("iat", "1704304355"),
                    new Claim("exp", "1704390755"),
                    new Claim("scope", "")
                };
                var trickyScopeClaims = new[] {
                    new Claim("user", "gabor"),
                    new Claim("iat", "1704304355"),
                    new Claim("exp", "1704390755"),
                    new Claim("scope", $"a{Scopes.Read}1 b{Scopes.Write}2")
                };
                var trickyUppercaseScopeClaims = new[] {
                    new Claim("user", "gabor"),
                    new Claim("iat", "1704304355"),
                    new Claim("exp", "1704390755"),
                    new Claim("scope", $"a{Scopes.Read.ToUpper()}1 b{Scopes.Write.ToUpper()}2")
                };

                yield return [false, Scopes.Read, noScopeClaims];
                yield return [false, Scopes.Write, noScopeClaims];
                yield return [false, Scopes.Read, scopeClaimWithoutPermissions];
                yield return [false, Scopes.Write, scopeClaimWithoutPermissions];
                yield return [true, Scopes.Read, scopeClaimWithReadPermission];
                yield return [false, Scopes.Write, scopeClaimWithReadPermission];
                yield return [false, Scopes.Read, scopeClaimWithWritePermission];
                yield return [true, Scopes.Write, scopeClaimWithWritePermission];
                yield return [true, Scopes.Read, scopeClaimWithReadAndWritePermissions];
                yield return [true, Scopes.Write, scopeClaimWithReadAndWritePermissions];
                yield return [false, Scopes.Read, emptyScopeClaim];
                yield return [false, Scopes.Write, emptyScopeClaim];
                yield return [false, Scopes.Read, trickyScopeClaims];
                yield return [false, Scopes.Write, trickyScopeClaims];
                yield return [false, Scopes.Read, trickyUppercaseScopeClaims];
                yield return [false, Scopes.Write, trickyUppercaseScopeClaims];
            }
        }

        [Theory]
        [MemberData(nameof(GetTestData))]
        public void IsScopeInClaims(bool isThere, string scope, IEnumerable<Claim> claims)
        {
            claims.HasScope(scope).Should().Be(isThere);
        }
    }
}
