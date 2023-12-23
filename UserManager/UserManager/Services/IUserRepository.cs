using UserManager.Dtos;

namespace UserManager.Services
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll(CancellationToken cancellation = default);
    }
}
