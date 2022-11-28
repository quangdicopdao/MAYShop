using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAYShop.Models
{
    public class Mail
    {
        public class MailModel
        {
            public string From
            {
                get;
                set;
            }
            public string To
            {
                get;
                set;
            }
            public string Subject
            {
                get;
                set;
            }
            public string Body
            {
                get;
                set;
            }
        }
    }
}