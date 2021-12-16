using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDeApplication.Models.Dto
{
    public class TotalItem
    {
        public string Name { get; set; }
        public string ItemCd { get; set; }
        public int Total { get; set; }
        public string receivedTime { get; set; }
        public string Address { get; set; }
    }
}
