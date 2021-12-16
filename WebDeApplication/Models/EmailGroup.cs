using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebDeApplication.Models
{

    public class EmailGroup
    {
        public int Id { get; set; }
        public int EmailId { get; set; }

        public string ODNumber { get; set; }
        public string ODParrent { get; set; }

        public string toAddress { get; set; }
        public string receivedTime { get; set; }

        public string fromAddress { get; set; }
        public string status { get; set; }
        public string address { get; set; }
        public string shippto { get; set; }
        public string tracking { get; set; }
        public string name { get; set; }
        public string orderTotal { get; set; }
        public DateTime received { get; set; }
        public Boolean shipped { get; set; }
        public DateTime estimatime { get; set; }

    }
}
