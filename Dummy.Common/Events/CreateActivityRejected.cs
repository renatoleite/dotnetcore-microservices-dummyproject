using System;

namespace Dummy.Common.Events
{
    public class CreateActivityRejected : IRejectedEvent
    {
        public Guid Id { get; }
        public string Reason { get; }
        public string Code { get; }

        protected CreateActivityRejected()
        {
        }

        public CreateActivityRejected(
            Guid id,
            string reason,
            string code)
        {
            this.Id = id;
            this.Reason = reason;
            this.Code = code;
        }
    }
}
