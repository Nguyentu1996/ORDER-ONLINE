using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDeApplication.Models.Dto
{
    public class ReadEmailAction
    {
        public string code { get; set; }
        public int limit { get; set; }
    }
}
