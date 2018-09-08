using System;

namespace Sample.Core.Models
{
    public class User : EntityBase
    {
        public Guid UserGuid { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string MobileNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string SecurityStamp { get; set; }
        public bool MobileNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool LockoutEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public long AccessFailedCount { get; set; }
        public bool IsEmailAlreadyExist { get; set; }
    }
}
