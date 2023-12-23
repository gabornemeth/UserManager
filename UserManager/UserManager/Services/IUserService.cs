using UserManager.Dtos;

namespace UserManager.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll(CancellationToken cancellation = default);
        
        Task<User?> Get(int id);
        Task Add(User user);
        Task Update(User user);
        Task Delete(int id);
    }
}
