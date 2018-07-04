using Sample.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Core.Repositories
{
    public interface IUserRepository 
    {
        User AuthenticateUser(string EmailAddress, string Password);
        User GetUser(Guid userGuid);
    }
}
