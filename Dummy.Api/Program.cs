using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dummy.Common.Events;
using Dummy.Common.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Dummy.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Startup the application and subcribe events.
            ServiceHost.Create<Startup>(args)
                .UseRabbitMq()
                .SubscribeToEvent<ActivityCreated>()
                .Build()
                .Run();
        }
    }
}
