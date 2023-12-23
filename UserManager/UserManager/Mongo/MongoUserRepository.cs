using AutoMapper;
using MongoDB.Driver;
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

        public async Task<IEnumerable<Dtos.User>> GetAll(CancellationToken cancellation)
        {
            var usersCollection = GetUsersCollection();
            var filter = Builders<User>.Filter.Empty;
            var users = await usersCollection.Find(filter).ToListAsync(cancellation);
            return users.Select(_mapper.Map<Dtos.User>);
        }

        public async Task<Dtos.User?> Get(int id, CancellationToken cancellation)
        {
            var usersCollection = GetUsersCollection();
            var user = await usersCollection.Find(u => u.Id == id).FirstOrDefaultAsync(cancellation);
            return user != null ? _mapper.Map<Dtos.User>(user) : null;
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
                        var userDb = _mapper.Map<User>(u);
                        BeforeInsert(userDb, usersCollection);
                        return userDb;
                    });

                    usersCollection.InsertMany(usersToAdd);
                }
            }
            catch (Exception ex)
            {
            }
        }


        private int _nextId = 0;

        private void BeforeInsert(User user, IMongoCollection<User> users)
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

            user.Id = _nextId++;
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

        public async Task Delete(Dtos.User user, CancellationToken cancellation = default)
        {
            var usersCollection = GetUsersCollection();
            await usersCollection.DeleteOneAsync(u => u.Id == user.Id);
        }

        public async Task Update(Dtos.User user, CancellationToken cancellation = default)
        {
            var usersCollection = GetUsersCollection();
            await usersCollection.UpdateOneAsync(u => u.Id == user.Id, new UpdateDefinition<User>())
        }

        public async Task Add(Dtos.User user, CancellationToken cancellation = default)
        {
            var usersCollection = GetUsersCollection();
            await usersCollection.InsertOneAsync(_mapper.)
        }
    }
}
