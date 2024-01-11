using UserManager.Contracts.Dtos;

namespace UserManager.Models
{
    /// <summary>
    /// EXPERIMENTAL:
    /// Common interface for <see cref="User"/> and <see cref="UserDtoBase"/>.
    /// It is used for common validation. Can be extended for other entities too.
    /// </summary>
    public interface IUser
    {
        public string Name { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
    }
}
