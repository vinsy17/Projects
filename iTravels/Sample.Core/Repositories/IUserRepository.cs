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
        User Register(User user);
        bool IsAccountExists(string emailAddress);
        bool ChangePassword(Guid userId, string currentPassword, string newPassword);
    }
}
