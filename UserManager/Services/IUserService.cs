using UserManager.Models;

namespace UserManager.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll(CancellationToken cancellation = default);
        
        Task<User?> Get(string id, CancellationToken cancellationToken = default);
        Task<bool> Create(User user, CancellationToken cancellationToken = default);
        Task<bool> Update(User user, CancellationToken cancellationToken = default);
        Task<bool> Delete(string id, CancellationToken cancellationToken = default);
    }
}
