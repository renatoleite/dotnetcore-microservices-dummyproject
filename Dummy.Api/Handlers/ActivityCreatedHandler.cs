using Dummy.Common.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Dummy.Api.Handlers
{
    public class ActivityCreatedHandler : IEventHandler<ActivityCreated>
    {
        public async Task HandleAsync(ActivityCreated @event)
        {
            await Task.CompletedTask;
            Console.WriteLine($"Activity created: {@event.Name}");
        }
    }
}
