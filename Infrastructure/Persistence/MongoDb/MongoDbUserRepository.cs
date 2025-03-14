using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UserManagementApi.Domain.Models;
using UserManagementApi.Domain.Ports;
using UserManagementApi.Infrastructure;

namespace UserManagementApi.Infrastructure.Persistence.MongoDb
{
    public class MongoDbUserRepository : IUserRepository
    {
        private readonly IMongoCollection<MongoDbUser> _usersCollection;

        public MongoDbUserRepository(IOptions<Infrastructure.DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _usersCollection = mongoDatabase.GetCollection<MongoDbUser>(databaseSettings.Value.UsersCollectionName);
        }

        public async Task<List<User>> GetAllAsync()
        {
            var users = await _usersCollection.Find(_ => true).ToListAsync();
            return users.Select(u => u.ToDomain()).ToList();
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            var user = await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return user?.ToDomain();
        }

        public async Task CreateAsync(User user)
        {
            var mongoUser = MongoDbUser.FromDomain(user);
            await _usersCollection.InsertOneAsync(mongoUser);
            
            // Asignar el ID generado por MongoDB al objeto de dominio
            user.Id = mongoUser.Id;
        }

        public async Task UpdateAsync(string id, User user)
        {
            var mongoUser = MongoDbUser.FromDomain(user);
            await _usersCollection.ReplaceOneAsync(x => x.Id == id, mongoUser);
        }

        public async Task DeleteAsync(string id)
        {
            await _usersCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}