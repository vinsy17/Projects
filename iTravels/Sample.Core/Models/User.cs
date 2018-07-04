using System;

namespace Sample.Core.Models
{
    public class User : EntityBase
    {
        public Guid UserGuid { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }

    }
}
