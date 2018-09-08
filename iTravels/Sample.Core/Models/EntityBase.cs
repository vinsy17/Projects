using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Core.Models
{
    public class EntityBase
    {
        public Guid UserId { get; set; }
        public bool IsAuthenticateUser { get; set; }// = true;
        public bool IsDeleted { get; set; }
        public DateTime CreateTimeStamp { get; set; }
        public DateTime UpdateTimeStamp { get; set; }
    }
}
