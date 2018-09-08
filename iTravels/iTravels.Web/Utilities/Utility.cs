using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iTravels.Web.Utilities
{
    public static class Utility
    {
        public static string GetFullName(string FirstName, string LastName)
        {
            string fullname = string.Empty;
            if (!string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName))
            {
                fullname = String.Format("{0} {1}", FirstName.Trim(), LastName.Trim());
            }
            else if (!string.IsNullOrWhiteSpace(FirstName))
            {
                fullname = FirstName.Trim();
            }
            else if (!string.IsNullOrWhiteSpace(LastName))
            {
                fullname = LastName.Trim();
            }
            return fullname;
        }

        public static Guid GetUserIdFromSession()//string FirstName, string LastName)
        {
            Guid UserId=new Guid();
            if (System.Web.HttpContext.Current.Session["UserId"] != null && !string.IsNullOrWhiteSpace(System.Web.HttpContext.Current.Session["UserId"].ToString()))
            {
                UserId = new Guid(System.Web.HttpContext.Current.Session["UserId"].ToString());
            }
            else
            {
                
            }
            return UserId;
        }
        public static string GetEmailAddressFromSession()//string FirstName, string LastName)
        {
            string EmailAddress = string.Empty;
            if (System.Web.HttpContext.Current.Session["EmailAddress"]!=null && !string.IsNullOrWhiteSpace(System.Web.HttpContext.Current.Session["EmailAddress"].ToString()))
            {
                EmailAddress = System.Web.HttpContext.Current.Session["EmailAddress"].ToString();
            }
            return EmailAddress;
        }
    }
}