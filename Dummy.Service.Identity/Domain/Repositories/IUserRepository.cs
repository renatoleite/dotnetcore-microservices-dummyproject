using Dummy.Service.Identity.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Dummy.Service.Identity.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task AddAsync(User user);
    }
}
