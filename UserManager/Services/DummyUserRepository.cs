using AutoMapper;
using UserManager.Models;

namespace UserManager.Services
{
    public class DummyUserRepository : IUserRepository
    {
        private List<User> _users = new();
        private readonly IMapper _mapper;

        public DummyUserRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task Create(User user, CancellationToken cancellation = default)
        {
            _users?.Add(user);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<User>> GetAll(CancellationToken cancellation)
        {
            return Task.FromResult<IEnumerable<User>>(_users);
        }

        public Task<User?> Get(int id, CancellationToken cancellation = default)
        {
            return Task.FromResult(GetUserById(id));
        }

        public Task<bool> Delete(User user, CancellationToken cancellation = default)
        {
            var userToDelete = GetUserById(user.Id);
            if (userToDelete != null)
            {
                _users.Remove(userToDelete);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        public Task Update(User user, CancellationToken cancellation = default)
        {
            var userToUpdate = GetUserById(user.Id);
            if (userToUpdate == null)
            {
                // error
            }
            else
            {
                _users.Remove(userToUpdate);
                _users.Add(user);
            }

            return Task.CompletedTask;
        }

        private User? GetUserById(int id) => _users?.FirstOrDefault(u => u.Id == id);

        public void Seed()
        {
            _users = SampleData.GetUsers()
                .Select(_mapper.Map<User>)
                .ToList();
        }
    }
}
