using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace WebDeApplication.Models
{

    public class EmailCancel
    {
        public int Id { get; set; }
        public string ODNumber { get; set; }
        public int? ODParrent { get; set; }

        public string Status { get; set; }

        public string Name { get; set; }
        public string Shippto { get; set; }
        public DateTime ReceivedTimeFD { get; set; }
        public string ReceivedTime { get; set; }

    }
}
