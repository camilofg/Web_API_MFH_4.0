using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_API_MFH_4._0.Models
{
    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool? EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecuritySalt { get; set; }
        public string PhoneNumber { get; set; }
        public int AccessFailedCount { get; set; }
        public string ValidEmailTempToken { get; set; }
    }

    public class OptionsMenu
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string BackGroundColor { get; set; }
        public bool IsHeader { get; set; }
        public OptionsMenu() { }
    }
}