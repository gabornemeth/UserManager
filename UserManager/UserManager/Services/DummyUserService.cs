using Newtonsoft.Json;
using UserManager.Dtos;
using UserManager.Helpers;

namespace UserManager.Services
{
    public class DummyUserService : IUserService
    {
        private readonly List<User> _users;

        public DummyUserService()
        {
            _users = SampleData.GetUsers().ToList();
        }

        public Task Add(User user)
        {
            _users.Add(user);
            return Task.CompletedTask;
        }

        public Task Delete(int userId)
        {
            var userToDelete = GetUserById(userId);
            if (userToDelete != null)
            {
                _users.Remove(userToDelete);
            }

            return Task.CompletedTask;
        }

        public Task<IEnumerable<User>> GetAll(CancellationToken cancellation)
        {
            return Task.FromResult<IEnumerable<User>>(_users);
        }

        public Task<User?> Get(int id)
        {
            return Task.FromResult(GetUserById(id));
        }

        public Task Update(User user)
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

        private User? GetUserById(int userId) => _users.FirstOrDefault(u => u.Id == userId);
    }
}
