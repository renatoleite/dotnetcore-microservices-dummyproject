﻿using Dummy.Service.Identity.Domain.Models;
using Dummy.Service.Identity.Domain.Repositories;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;

namespace Dummy.Service.Identity.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDatabase _database;

        public UserRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<User> GetAsync(Guid id)
            => await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<User> GetAsync(string email)
            => await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Email == email.ToLowerInvariant());

        public async Task AddAsync(User user)
            => await Collection.InsertOneAsync(user);

        private IMongoCollection<User> Collection
            => _database.GetCollection<User>("Users");
    }
}
