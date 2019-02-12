using System;

namespace Dummy.Common.Events
{
    public interface IAuthenticatedEvent : IEvent
    {
        Guid UserId { get; }
    }
}
