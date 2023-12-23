using AutoMapper;
using MongoDB.Bson;
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
            return users.Select(u => _mapper.Map<Dtos.User>(u));
        }

        private IMongoCollection<User> GetUsersCollection()
        {
            var client = new MongoClient(_configuration["MongoDB:ConnectionString"]);
            var database = client.GetDatabase(_configuration["MongoDB:Database"]);
            return database.GetCollection<User>("users");
        }
    }
}
