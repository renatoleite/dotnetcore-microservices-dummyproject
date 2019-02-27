using Dummy.Api.Controllers;
using Dummy.Api.Repositories;
using Dummy.Common.Commands;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Dummy.Api.Tests.Unit.Controller
{
    public class ActivitiesControllerTests
    {
        [Fact]
        public async Task activities_controller_post_should_return_accepted()
        {
            // Mocks.
            var busClientMock = new Mock<IBusClient>();
            var activityRepositoryMock = new Mock<IActivityRepository>();

            var controller = new ActivitiesController(busClientMock.Object, activityRepositoryMock.Object);
            var userId = Guid.NewGuid();

            // Creating controller context and adding user authenticated.
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, userId.ToString())
                    }, "test"))
                }
            };

            var command = new CreateActivity
            {
                Id = Guid.NewGuid(),
                UserId = userId
            };

            var result = await controller.Post(command);

            var contentResult = result as AcceptedResult;

            contentResult.Should().NotBeNull();
            contentResult.Location.Should().BeEquivalentTo($"activities/{command.Id}/{command.Name}");
        }
    }
}
