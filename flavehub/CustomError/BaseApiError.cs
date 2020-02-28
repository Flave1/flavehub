using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flavehub.CustomError
{
    public class BaseApiError
    {
        public int StatusCode { get; private set; }
        public string StatusDescription { get; private set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Friendly_Message { get; private set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Technical_Message { get; set; }

        public BaseApiError(int statusCode, string statusCodeDescripton)
        {
            StatusCode = statusCode;
            StatusDescription = statusCodeDescripton;
        }

        public BaseApiError(int statusCode, string statusCodeDescripton, string F_message) : this(statusCode, statusCodeDescripton)
        {
            Friendly_Message = F_message;
        }
        public BaseApiError(int statusCode, string statusCodeDescripton, string F_message, string T_message) : this(statusCode, statusCodeDescripton)
        {
            Friendly_Message = F_message;
            Technical_Message = T_message;
        }
    }
}
