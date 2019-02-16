using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dummy.Common.Commands;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace Dummy.Api.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IBusClient _busClient;

        public UsersController(IBusClient busClient)
        {
            this._busClient = busClient;
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]CreateUser command)
        {
            await _busClient.PublishAsync(command);

            return Accepted();
        }
    }
}