using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCommerce_Asp.Net_MVC.Models
{
    public class UserInfo
    {
        public int UserId { get; set; }
        public int CorporateAccountId { get; set; }
        public int CustomerAccountId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CorporateName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string WebSite { get; set; }
        public string Role { get; set; }
        public bool Status { get; set; }
    }
}