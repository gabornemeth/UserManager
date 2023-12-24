using UserManager.Models;

namespace UserManager.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll(CancellationToken cancellation = default);
        
        Task<User?> Get(int id);
        Task<bool> Add(User user);
        Task<bool> Update(User user);
        Task<bool> Delete(int id);
    }
}
