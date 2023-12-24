using UserManager.Contracts.Dtos;

namespace UserManager.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAll(CancellationToken cancellation = default);
        
        Task<UserDto?> Get(int id);
        Task<bool> Add(UserDto user);
        Task<bool> Update(UserDto user);
        Task<bool> Delete(int id);
    }
}
