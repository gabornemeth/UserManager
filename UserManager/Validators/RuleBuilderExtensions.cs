using Amazon.SecurityToken.Model.Internal.MarshallTransformations;
using FluentValidation;
using System.Text.RegularExpressions;
using UserManager.Models;

namespace UserManager.Validators
{
    internal static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<TUser, string> PersonName<TUser>(this IRuleBuilder<TUser, string> ruleBuilder) where TUser : IUser
        {
            return ruleBuilder.Must(IsPersonName);
        }

        public static IRuleBuilderOptions<TUser, string?> Uri<TUser>(this IRuleBuilder<TUser, string?> ruleBuilder) where TUser : IUser
        {
            return ruleBuilder.Must(IsUri);
        }

        private static bool IsPersonName(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;
            var match = Regex.Match(name, "^([a-z]|[A-Z]|\\s|\\d)+$");
            return match.Success;
        }

        private static bool IsUri(string? uri)
        {
            if (string.IsNullOrEmpty(uri)) return false;

            try
            {
                new Uri(uri);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
