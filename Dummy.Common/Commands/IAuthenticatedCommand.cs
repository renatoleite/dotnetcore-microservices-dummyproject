using System;
using System.Collections.Generic;
using System.Text;

namespace Dummy.Common.Commands
{
    /// <summary>
    /// Commands authenticated.
    /// </summary>
    public interface IAuthenticatedCommand : ICommand
    {
        /// <summary>
        /// User id that is currently authenticated.
        /// </summary>
        Guid UserId { get; set; }
    }
}
