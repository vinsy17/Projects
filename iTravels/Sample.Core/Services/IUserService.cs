using Sample.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Core.Services
{
    public interface IUserService
    {
        //User AuthenticateUser(string EmailAddress, string Password);
        //User ValidateUser(string EmailAddress, string Password);
        User AuthenticateUser(string EmailAddress, string Password);
        User GetUser(Guid userGuid);
        User Register(User user);
        bool IsAccountExists(string emailAddress);
        bool ChangePassword(Guid userId, string currentPassword, string newPassword);

    }
}
