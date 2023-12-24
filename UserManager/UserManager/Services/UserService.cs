using AutoMapper;
using UserManager.Dtos;
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

        public async Task<bool> Add(UserDto user)
        {
            var existingUser = await _repository.Get(user.Id);
            if (existingUser != null)
            {
                return false;
            }

            // TODO: validate some fields

            await _repository.Add(_mapper.Map<User>(user));
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

        public async Task<IEnumerable<UserDto>> GetAll(CancellationToken cancellation)
        {
            var users = await _repository.GetAll(cancellation);
            return users.Select(_mapper.Map<UserDto>);
        }

        public async Task<UserDto?> Get(int id)
        {
            var user = await _repository.Get(id);
            if (user == null) return null;

            return _mapper.Map<UserDto>(user);
        }

        public async Task<bool> Update(UserDto user)
        {
            var userToUpdate = await _repository.Get(user.Id);
            if (userToUpdate == null)
            {
                return false;
            }

            // TODO: validate some fields

            await _repository.Update(_mapper.Map<User>(user));
            return true;
        }
    }
}
