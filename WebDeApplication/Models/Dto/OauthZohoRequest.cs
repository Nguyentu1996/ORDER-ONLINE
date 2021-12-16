using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDeApplication.Models.Dto
{
    public class OauthZohoRequest
    {

        public string code { get; set; }
        public string redirect_uri { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string grant_type { get; set; }
    }
}
