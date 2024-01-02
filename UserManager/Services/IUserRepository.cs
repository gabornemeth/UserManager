using UserManager.Models;

namespace UserManager.Services
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll(CancellationToken cancellation = default);
        Task<User?> Get(int id, CancellationToken cancellation = default);
        Task<bool> Delete(User user, CancellationToken cancellation = default);
        Task<bool> Update(User user, CancellationToken cancellation = default);
        Task Create(User user, CancellationToken cancellation = default);
        void Seed();
    }
}
