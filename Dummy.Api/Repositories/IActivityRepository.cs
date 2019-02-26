using Dummy.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dummy.Api.Repositories
{
    public interface IActivityRepository
    {
        Task<Activity> GetAsync(Guid id);
        Task<IEnumerable<Activity>> SearchAsync(Guid userId);
        Task AddAsync(Activity activity);
    }
}
