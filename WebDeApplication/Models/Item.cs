using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDeApplication.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ItemCd { get; set; }
        public string ODnumber { get; set; }
        public string MessageId { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public string receiveiTime { get; set; }
        public long receiveiTimeLong { get; set; }
        public string Address { get; set; }
        public string Price { get; set; }
    }
}
