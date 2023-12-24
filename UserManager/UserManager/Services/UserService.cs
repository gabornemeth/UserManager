using AutoMapper;
using UserManager.Contracts.Dtos;
using UserManager.Models;

namespace UserManager.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Add(User user)
        {
            var existingUser = await _repository.Get(user.Id);
            if (existingUser != null)
            {
                return false;
            }

            // TODO: validate some fields

            await _repository.Add(user);
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var user = await _repository.Get(id);
            if (user == null)
            {
                return false;
            }

            return await _repository.Delete(user);
        }

        public Task<IEnumerable<User>> GetAll(CancellationToken cancellation)
        {
            return _repository.GetAll(cancellation);
        }

        public Task<User?> Get(int id)
        {
            return _repository.Get(id);
        }

        public async Task<bool> Update(User user)
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
