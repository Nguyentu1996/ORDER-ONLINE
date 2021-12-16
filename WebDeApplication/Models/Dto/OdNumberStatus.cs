using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDeApplication.Models.Dto
{
    public class OdNumberStatus
    {
        public int Id { get; set; }
        public bool Shipped { get; set; }
        public string OdNumber { get; set; }
    }
}
