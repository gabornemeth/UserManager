using AutoMapper;
using MongoDB.Driver;
using UserManager.Models;
using UserManager.Services;

namespace UserManager.Mongo
{
    public class MongoUserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public MongoUserRepository(IConfiguration config, IMapper mapper)
        {
            _configuration = config;
            _mapper = mapper;
        }

        public async Task<IEnumerable<User>> GetAll(CancellationToken cancellation)
        {
            var usersCollection = GetUsersCollection();
            var filter = Builders<User>.Filter.Empty;
            var users = await usersCollection.Find(filter).ToListAsync(cancellation);
            return users;
        }

        public async Task<User?> Get(int id, CancellationToken cancellation)
        {
            var usersCollection = GetUsersCollection();
            var user = await usersCollection.Find(u => u.Id == id).FirstOrDefaultAsync(cancellation);
            return user;
        }

        public void Seed()
        {
            try
            {
                var usersCollection = GetUsersCollection();
                var count = usersCollection.CountDocuments(_ => true);
                if (count == 0)
                {
                    var db = GetDatabase();

                    var usersToAdd = SampleData.GetUsers().Select(u =>
                    {
                        var user = _mapper.Map<User>(u);
                        user.Id = GetNextId(usersCollection);
                        return user;
                    });

                    usersCollection.InsertMany(usersToAdd);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private IMongoDatabase GetDatabase()
        {
            var client = new MongoClient(_configuration["MongoDB:ConnectionString"]);
            return client.GetDatabase(_configuration["MongoDB:Database"]);
        }

        private IMongoCollection<User> GetUsersCollection()
        {
            var database = GetDatabase();
            return database.GetCollection<User>("users");
        }

        public async Task<bool> Delete(User user, CancellationToken cancellation = default)
        {
            var usersCollection = GetUsersCollection();
            var result = await usersCollection.DeleteOneAsync(u => u.Id == user.Id);
            return result.DeletedCount == 1;
        }

        public async Task Update(User user, CancellationToken cancellation = default)
        {
            var usersCollection = GetUsersCollection();
            await usersCollection.InsertOneAsync(user); // it should overwrite the existing one
        }

        public async Task Create(User user, CancellationToken cancellation = default)
        {
            var usersCollection = GetUsersCollection();
            await usersCollection.InsertOneAsync(user);
        }

        private int _nextId = 0;

        private int GetNextId(IMongoCollection<User> users)
        {
            if (_nextId == 0)
            {
                try
                {
                    _nextId = users.Find(_ => true).ToEnumerable().Max(u => u.Id) + 1;
                }
                catch
                {
                    _nextId = 1; // collection was empty
                }
            }

            return _nextId++;
        }
    }
}
