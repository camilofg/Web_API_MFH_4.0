using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_API_MFH_4._0.Models
{
    public class TokenUser 
    {
        public User SessionUser { get; set; }
        public string ValidationToken { get; set; }
        public string Msg { get; set; }
    }

    public class UserToken
    {
        public int ID { get; set; }
        public string UsID { get; set; }
        public string UsToken { get; set; }
    }

    public class RememberPass {
        public string UserId { get; set; }
        public string NewPassword { get; set; }
    }
}