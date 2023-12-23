using UserManager.Dtos;

namespace UserManager.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task Add(User user)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAll(CancellationToken cancellation)
        {
            return _repository.GetAll(cancellation);
        }

        public Task<User?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
