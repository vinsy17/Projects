using Sample.Core.Models;
using Sample.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        public User AuthenticateUser(string EmailAddress, string Password)
        {
            User _user = new User();
            using (iTravelsEntities entities = new iTravelsEntities())
            {

                var _dbUser = (from u in entities.it_User
                               where u.Email_Address == EmailAddress && u.Password==Password && u.Is_Deleted == false
                               select new User
                               {
                                   UserGuid = u.User_Id,
                                   EmailAddress = u.Email_Address,
                                   MobileNumber = u.Mobile_Number,
                                   FirstName = u.First_Name,
                                   LastName = u.Last_Name,
                                   DisplayName = u.Display_Name
                               }).FirstOrDefault();
                if (_dbUser != null)
                {
                    _user = _dbUser;
                }
                else
                {
                    _user.IsAuthenticateUser = false;
                }
            }
            return _user;
        }
        public User GetUser(Guid userGuid)
        {
            User _user = new User();
            using (iTravelsEntities entities = new iTravelsEntities())
            {
                var _dbUser = (from u in entities.it_User
                               where u.User_Id == userGuid && u.Is_Deleted == false
                               select new User
                               {
                                   UserGuid = u.User_Id,
                                   EmailAddress = u.Email_Address,
                                   MobileNumber = u.Mobile_Number,
                                   FirstName = u.First_Name,
                                   LastName = u.Last_Name,
                                   DisplayName = u.Display_Name
                               }).FirstOrDefault();
                if (_dbUser != null)
                {
                    _user = _dbUser;
                }
                return _user;
            }
        }

    }
}
