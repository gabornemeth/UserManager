using UserManager.Dtos;

namespace UserManager.Services
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll(CancellationToken cancellation = default);
        Task<User?> Get(int id, CancellationToken cancellation = default);
        Task Delete(User user, CancellationToken cancellation = default);
        Task Update(User user, CancellationToken cancellation = default);
        Task Add(User user, CancellationToken cancellation = default);
        void Seed();
    }
}
