using Sample.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Core.Services
{
    public interface IUserService
    {
        User AuthenticateUser(string EmailAddress, string Password);
        User GetUser(Guid userGuid);
        User Register(User user);
    }
}
