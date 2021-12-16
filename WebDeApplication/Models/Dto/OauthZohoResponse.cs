using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDeApplication.Models.Dto
{
    public class OauthZohoResponse
    {
        public string expires_in { get; set; }
        public string token_type { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
    }
}
