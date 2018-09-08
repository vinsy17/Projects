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
                var dbUserDetails = entities != null && entities.it_User != null && entities.it_User.Count() > 0 ? entities.it_User.FirstOrDefault(x => x.Email_Address.ToLower() == EmailAddress.ToLower().Trim() && !x.Is_Deleted) : null;
                if (dbUserDetails != null && dbUserDetails.User_Id != new Guid() && !string.IsNullOrWhiteSpace(dbUserDetails.Password_Salt))
                {
                    //Password Hasing Process Call Helper Class Method    
                    string encodingPassword = HashHelper.EncodePassword(Password, dbUserDetails.Password_Salt);
                    //Check Login Detail User Name Or Password
                    it_User dbUser = entities != null && entities.it_User != null && entities.it_User.Count() > 0 ? entities.it_User.FirstOrDefault(x => x.Email_Address.ToLower() == EmailAddress.ToLower().Trim() && x.Password == encodingPassword && !x.Is_Deleted) : null;

                    if (dbUser != null && dbUser.User_Id != new Guid())
                    {
                        if (dbUser != null && dbUser.User_Id != new Guid() && dbUser.Is_Lockout_Enabled)
                        {
                            _user.LockoutEnabled = true;
                        }
                        else
                        {
                            _user = new User()
                            {
                                UserGuid = dbUser.User_Id,
                                EmailAddress = dbUser.Email_Address,
                                MobileNumber = dbUser.Mobile_Number,
                                FirstName = dbUser.First_Name,
                                LastName = dbUser.Last_Name,
                                DisplayName = dbUser.Display_Name,
                                IsAuthenticateUser = true
                            };
                        }
                    }
                    else
                    {
                        _user.IsAuthenticateUser = false;
                    }
                }
                else
                {
                    _user.IsAuthenticateUser = false;
                }

            }
            return _user;
        }
        public User GetUser(Guid userId)
        {
            User _user = new User();
            using (iTravelsEntities entities = new iTravelsEntities())
            {
                var _dbUser = (from u in entities.it_User
                               where u.User_Id == userId && u.Is_Deleted == false
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
        public User Register(User user)
        {
            using (iTravelsEntities entities = new iTravelsEntities())
            {
                if (entities != null && entities.it_User != null)
                {
                    it_User dbUser = entities.it_User.Count() > 0 ? entities.it_User.FirstOrDefault(x => x.Email_Address.ToLower() == user.EmailAddress.ToLower().Trim() && !x.Is_Deleted) : null;
                    if (dbUser != null && dbUser.User_Id != new Guid() && dbUser.Is_Lockout_Enabled)
                    {
                        user.LockoutEnabled = true;
                    }
                    else if (dbUser != null && dbUser.User_Id != new Guid())
                    {
                        user.IsEmailAlreadyExist = true;
                    }
                    else
                    {
                        dbUser = new it_User();
                        dbUser.User_Id = Guid.NewGuid();
                        dbUser.Email_Address = user.EmailAddress;
                        var keySalt = HashHelper.GeneratePassword(10);//ref url: https://www.c-sharpcorner.com/UploadFile/145c93/save-password-using-salted-hashing/
                        string encodedPassword = HashHelper.EncodePassword(user.Password, keySalt);
                        dbUser.Password_Salt = keySalt;
                        dbUser.Password = encodedPassword;
                        dbUser.First_Name = user.FirstName;
                        dbUser.Last_Name = user.LastName;
                        dbUser.Mobile_Number = user.MobileNumber;
                        dbUser.Display_Name = user.DisplayName;// != default(DateTime) ? user.PostedOn : default(DateTime?);
                        dbUser.Security_Stamp = user.SecurityStamp;
                        dbUser.Is_Email_Confirmed = false;
                        dbUser.Is_Mobile_Number_Confirmed = false;//user.MobileNumberConfirmed;
                        dbUser.Is_Two_Factor_Enabled = false;// user.TwoFactorEnabled;
                        dbUser.Is_Lockout_Enabled = false;// user.LockoutEnabled;
                        dbUser.Lockout_End_Date_Utc = DateTime.Now.AddYears(1);// user.LockoutEndDateUtc;
                        dbUser.Access_Failed_Count = 0;// user.AccessFailedCount;
                        dbUser.Is_Deleted = default(bool);
                        dbUser.Create_Time_Stamp = DateTime.Now;
                        dbUser.Update_Time_Stamp = DateTime.Now;
                        entities.it_User.Add(dbUser);
                        entities.SaveChanges();
                        if (dbUser.User_Id != new Guid())
                        {
                            user.UserId = dbUser.User_Id;
                            user.IsAuthenticateUser = true;
                        }
                    }
                }
            }
            return user;
        }
        public bool IsAccountExists(string emailAddress)
        {
            bool isAccountExists = false;
            using (iTravelsEntities entities = new iTravelsEntities())
            {
                if (entities != null && entities.it_User != null)
                {
                    isAccountExists = entities.it_User.Count() > 0 && entities.it_User.Any(x => x.Email_Address.ToLower() == emailAddress.ToLower().Trim() && !x.Is_Deleted);

                }
            }
            return isAccountExists;
        }
        public bool ChangePassword(Guid userId, string currentPassword, string newPassword)
        {
            bool isSuccess = false;
            using (iTravelsEntities entities = new iTravelsEntities())
            {
                var dbUserDetails = entities != null && entities.it_User != null && entities.it_User.Count() > 0 ? entities.it_User.FirstOrDefault(x => x.User_Id == userId && !x.Is_Deleted) : null;
                if (dbUserDetails != null && dbUserDetails.User_Id != new Guid() && !string.IsNullOrWhiteSpace(dbUserDetails.Password_Salt))
                {
                    //Password Hasing Process Call Helper Class Method    
                    string encodedCurrentPassword = HashHelper.EncodePassword(currentPassword, dbUserDetails.Password_Salt);
                    //Check Login Detail User Name Or Password
                    it_User dbUser = (from i in entities.it_User
                                      where i.User_Id == userId && i.Password == encodedCurrentPassword && !i.Is_Deleted
                                      select i).FirstOrDefault();//entities != null && entities.it_User != null && entities.it_User.Count() > 0 ? entities.it_User.FirstOrDefault(x => x.User_Id == userId && x.Password == currentPassword && !x.Is_Deleted) : null;
                    if (dbUser != null && dbUser.User_Id != new Guid())
                    {
                        var keySalt = HashHelper.GeneratePassword(10);//ref url: https://www.c-sharpcorner.com/UploadFile/145c93/save-password-using-salted-hashing/
                        string encodedPassword = HashHelper.EncodePassword(newPassword, keySalt);
                        dbUser.Password_Salt = keySalt;
                        dbUser.Password = encodedPassword;
                        dbUser.Update_Time_Stamp = DateTime.Now;
                        entities.SaveChanges();
                        if (dbUser.User_Id != new Guid())
                        {
                            isSuccess = true;
                        }
                    }
                }
            }
            return isSuccess;
        }
    }
}
