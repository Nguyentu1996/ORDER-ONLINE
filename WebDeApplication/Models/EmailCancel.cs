using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace WebDeApplication.Models
{

    public class EmailCancel
    {
        [Key]
        public string ODNumber { get; set; }
        public string ODParrent { get; set; }

        public string Status { get; set; }

        public string Name { get; set; }
        public string Shippto { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
