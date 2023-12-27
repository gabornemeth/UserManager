using UserManager.Models;

namespace UserManager.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<bool> Create(User user, CancellationToken cancellationToken = default)
        {
            if (user.Id != 0)
            {
                var existingUser = await _repository.Get(user.Id, cancellationToken);
                if (existingUser != null)
                {
                    return false;
                }
            }

            // TODO: validate some fields

            await _repository.Create(user);
            return true;
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            var user = await _repository.Get(id, cancellationToken);
            if (user == null)
            {
                return false;
            }

            return await _repository.Delete(user);
        }

        public Task<IEnumerable<User>> GetAll(CancellationToken cancellation = default)
        {
            return _repository.GetAll(cancellation);
        }

        public Task<User?> Get(int id, CancellationToken cancellationToken = default)
        {
            return _repository.Get(id, cancellationToken);
        }

        public async Task<bool> Update(User user, CancellationToken cancellationToken = default)
        {
            var userToUpdate = await _repository.Get(user.Id);
            if (userToUpdate == null)
            {
                return false;
            }

            // TODO: validate some fields

            await _repository.Update(user);
            return true;
        }
    }
}
