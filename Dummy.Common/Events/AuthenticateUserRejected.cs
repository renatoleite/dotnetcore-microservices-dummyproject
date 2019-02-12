namespace Dummy.Common.Events
{
    public class AuthenticateUserRejected : IRejectedEvent
    {
        public string Email { get; }
        public string Code { get; }
        public string Reason { get; }

        protected AuthenticateUserRejected()
        {
        }

        public AuthenticateUserRejected(
            string email,
            string code,
            string reason)
        {
            this.Email = email;
            this.Code = code;
            this.Reason = reason;
        }
    }
}
