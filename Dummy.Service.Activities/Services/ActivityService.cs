using Dummy.Common.Exceptions;
using Dummy.Service.Activities.Domain.Models;
using Dummy.Service.Activities.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dummy.Service.Activities.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ActivityService(
            IActivityRepository activityRepository,
            ICategoryRepository categoryRepository)
        {
            _activityRepository = activityRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task AddAsync(
            Guid id,
            Guid userId,
            string category,
            string name,
            string description,
            DateTime createdAt)
        {
            var activityCategory = await _categoryRepository.GetAsync(category);

            if (activityCategory == null)
                throw new CustomException("category_not_found", $"Category: '{category}' was not found.");

            var activity = new Activity(id, activityCategory, userId, name, description, createdAt);

            await _activityRepository.AddAsync(activity);
        }
    }
}
