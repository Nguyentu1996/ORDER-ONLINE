using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebDeApplication.Models
{

    public class EmailReader
    {
        public int Id { get; set; }

        public string ODNumber { get; set; }
        public string summary { get; set; }
        public string sentDateInGMT { get; set; }
        public string subject { get; set; }
        public string messageId { get; set; }
        public string priority { get; set; }
        public string hasInline { get; set; }
        public string toAddress { get; set; }
        public string folderId { get; set; }
        public string ccAddress { get; set; }
        public string sender { get; set; }
        public string receivedTime { get; set; }
        public long receivedTimeLong { get; set; }

        public string fromAddress { get; set; }
        public string status { get; set; }
        public string status2 { get; set; }
        public Boolean isChecked { get; set; }
        public string orderDate { get; set; }
        public DateTime estimateDilivery { get; set; }
        public string address { get; set; }
        public string shippto { get; set; }
        public string tracking { get; set; }
        public string name { get; set; }
        public string orderTotal { get; set; }
        public int odParrent { get; set; }
        public bool shipped { get; set; }

    }
}
