using Dummy.Service.Activities.Domain.Models;
using Dummy.Service.Activities.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dummy.Service.Activities.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IMongoDatabase _database;

        public ActivityRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task AddAsync(Activity activity) =>
            await Collection.InsertOneAsync(activity);

        public async Task DeleteAsync(Guid id) =>
            await Collection.DeleteOneAsync(x => x.Id == id);

        public async Task<Activity> GetAsync(Guid id) =>
            await Collection
            .AsQueryable()
            .FirstOrDefaultAsync(x => x.Id == id);

        private IMongoCollection<Activity> Collection =>
            _database.GetCollection<Activity>("Activities");
    }
}
