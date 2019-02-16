using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dummy.Common.Commands;
using Dummy.Common.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Dummy.Service.Activities
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Startup and subscribe commands.
            ServiceHost.Create<Startup>(args)
                .UseRabbitMq()
                .SubscribeToCommand<CreateUser>()
                .Build()
                .Run();
        }
    }
}
