using UserManager.Dtos;

namespace UserManager.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        
        Task<User?> GetById(int id);
        Task Add(User user);
        Task Update(User user);
        Task Delete(int userId);
    }
}
