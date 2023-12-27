using UserManager.Models;

namespace UserManager.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll(CancellationToken cancellation = default);
        
        Task<User?> Get(int id, CancellationToken cancellationToken = default);
        Task<bool> Create(User user, CancellationToken cancellationToken = default);
        Task<bool> Update(User user, CancellationToken cancellationToken = default);
        Task<bool> Delete(int id, CancellationToken cancellationToken = default);
    }
}
