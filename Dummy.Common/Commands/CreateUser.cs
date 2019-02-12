using System;
using System.Collections.Generic;
using System.Text;

namespace Dummy.Common.Commands
{
    /// <summary>
    /// Model to create user.
    /// </summary>
    public class CreateUser : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
