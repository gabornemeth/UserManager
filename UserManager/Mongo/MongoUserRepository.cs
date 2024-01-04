using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;
using UserManager.Models;
using UserManager.Services;

namespace UserManager.Mongo
{
    public class MongoUserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly MongoClient _client;
        private readonly string _databaseName;

        public MongoUserRepository(string connectionString, string databaseName, IMapper mapper)
        {
            _client = new MongoClient(connectionString);
            _databaseName = databaseName;
            _mapper = mapper;
        }

        public async Task<IEnumerable<User>> GetAll(CancellationToken cancellation = default)
        {
            var usersCollection = GetUsersCollection();
            var filter = Builders<User>.Filter.Empty;
            var users = await usersCollection.Find(filter).ToListAsync(cancellation);
            return users;
        }

        public async Task<User?> Get(string id, CancellationToken cancellation = default)
        {
            var usersCollection = GetUsersCollection();
            var user = await usersCollection.Find(u => u.Id.ToString() == id).FirstOrDefaultAsync(cancellation);
            return user;
        }

        public void Seed()
        {
            var usersCollection = GetUsersCollection();
            var count = usersCollection.CountDocuments(_ => true);
            if (count == 0)
            {
                var db = GetDatabase();

                var usersToAdd = SampleData.GetUsers().Select(u =>
                {
                    var user = _mapper.Map<User>(u);
                    BeforeInsert(user);
                    return user;
                });

                usersCollection.InsertMany(usersToAdd);
            }
        }

        private IMongoDatabase GetDatabase()
        {
            return _client.GetDatabase(_databaseName);
        }

        private IMongoCollection<User> GetUsersCollection()
        {
            var database = GetDatabase();
            return database.GetCollection<User>("users");
        }

        public async Task<bool> Delete(User user, CancellationToken cancellation = default)
        {
            var usersCollection = GetUsersCollection();
            var result = await usersCollection.DeleteOneAsync(GetFilter(user));
            return result.DeletedCount == 1;
        }

        public async Task<bool> Update(User user, CancellationToken cancellation = default)
        {
            var usersCollection = GetUsersCollection();
            var update = Builders<User>.Update
                .Set(u => u.Name, user.Name)
                .Set(u => u.UserName, user.UserName)
                .Set(u => u.Email, user.Email)
                .Set(u => u.Website, user.Website)
                .Set(u => u.Phone, user.Phone)
                .Set(u => u.Address, user.Address)
                .Set(u => u.Company, user.Company);

            var result = await usersCollection.UpdateOneAsync(GetFilter(user), update);
            return result.ModifiedCount == 1;
        }

        public async Task Create(User user, CancellationToken cancellation = default)
        {
            var usersCollection = GetUsersCollection();
            BeforeInsert(user);
            await usersCollection.InsertOneAsync(user);
        }

        private void BeforeInsert(User user)
        {
            user.Id = null;
        }

        private Expression<Func<User, bool>> GetFilter(User user) => u => u.Id == user.Id;
    }
}
