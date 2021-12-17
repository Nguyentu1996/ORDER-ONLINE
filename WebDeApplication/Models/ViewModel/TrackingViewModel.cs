using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDeApplication.Models.ViewModel
{
    public class TrackingViewModel
    {
        public int Id { get; set; }
        public string Track { get; set; }
        public Boolean Shipped { get; set; }
    }
}
