using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dummy.Service.Activities.Domain.Models;
using Dummy.Service.Activities.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Dummy.Service.Activities.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoDatabase _database;

        public CategoryRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task AddAsync(Category category) =>
            await Collection.InsertOneAsync(category);

        public async Task<Category> GetAsync(string name) =>
            await Collection
            .AsQueryable()
            .FirstOrDefaultAsync(x => x.Name == name.ToLowerInvariant());

        public async Task<IEnumerable<Category>> SearchAsync() =>
            await Collection
            .AsQueryable()
            .ToListAsync();

        private IMongoCollection<Category> Collection
            => _database.GetCollection<Category>("Categories");
    }
}
