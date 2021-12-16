using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDeApplication.Models.Dto
{
    public class EmailAddress
    {
       
            public string isPrimary { get; set; }
            public string isConfirmed { get; set; }
            public string mailId { get; set; }
            public string isAlias { get; set; }
        
    }
}
