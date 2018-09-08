using Sample.Core.Models;
using Sample.Core.Repositories;
using Sample.Core.Services;
using Sample.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Services.Services
{
    public class UserService : IUserService
    {
        IUserRepository _repository = new UserRepository();
        public User AuthenticateUser(string EmailAddress, string Password)
        {
            //using (_repository = new UserRepository())
            //{
            return _repository.AuthenticateUser(EmailAddress, Password);
            //}
        }
        public User GetUser(Guid userId)
        {
            return _repository.GetUser(userId);
        }
        public User Register(User user)
        {
            return _repository.Register(user);
        }
        public bool IsAccountExists(string emailAddress)
        {
            return _repository.IsAccountExists(emailAddress);
        }
        public bool ChangePassword(Guid userId, string currentPassword, string newPassword)
        {
            return _repository.ChangePassword(userId, currentPassword, newPassword);
        }
    }
}
