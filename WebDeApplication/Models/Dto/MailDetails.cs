using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDeApplication.Models.Dto
{
    public class MailDetails
    {
        public string sendMailID { get; set; }
        public string fromAddress { get; set; }
        public string status { get; set; }
        public string mode { get; set; }
        public string displayName { get; set; }
        public string serverName { get; set; }
        public string serverPort { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string validated { get; set; }
        public string connectionType { get; set; }
        public string validationRequired { get; set; }
    }
}
