//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sample.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class it_User
    {
        public System.Guid User_Id { get; set; }
        public string Email_Address { get; set; }
        public bool Email_Confirmed { get; set; }
        public string Password { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Mobile_Number { get; set; }
        public string Display_Name { get; set; }
        public string Security_Stamp { get; set; }
        public bool Mobile_Number_Confirmed { get; set; }
        public bool Two_Factor_Enabled { get; set; }
        public bool Lockout_Enabled { get; set; }
        public Nullable<System.DateTime> Lockout_End_Date_Utc { get; set; }
        public long Access_Failed_Count { get; set; }
        public bool Is_Deleted { get; set; }
        public System.DateTime Create_Time_Stamp { get; set; }
        public System.DateTime Update_Time_Stamp { get; set; }
    }
}