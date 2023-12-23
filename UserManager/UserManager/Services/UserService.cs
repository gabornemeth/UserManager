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

        public async Task Delete(int id)
        {
            var user = await _repository.Get(id);
            if (user == null)
            {
                throw new ArgumentException();
            }
            await _repository.Delete(user);
        }

        public Task<IEnumerable<User>> GetAll(CancellationToken cancellation)
        {
            return _repository.GetAll(cancellation);
        }

        public Task<User?> Get(int id)
        {
            return _repository.Get(id);
        }

        public Task Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
