using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDeApplication.Models.Dto
{
    public class MailData
    {
        public string summary { get; set; }
        public string sentDateInGMT { get; set; }
        public int calendarType { get; set; }
        public string subject { get; set; }
        public string messageId { get; set; }
        public string flagid { get; set; }
        public string status2 { get; set; }
        public string priority { get; set; }
        public string hasInline { get; set; }
        public string toAddress { get; set; }
        public string folderId { get; set; }
        public string ccAddress { get; set; }
        public string hasAttachment { get; set; }
        public string size { get; set; }
        public List<string> labelId { get; set; }
        public string sender { get; set; }
        public string receivedTime { get; set; }
        public string fromAddress { get; set; }
        public string status { get; set; }
    }

}
