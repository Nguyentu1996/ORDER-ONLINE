using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDeApplication.Models.Dto
{
    public class Root
    {   
            public Status status { get; set; }
            public List<MailData> data { get; set; }
    }
}
