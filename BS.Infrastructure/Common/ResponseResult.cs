using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Infrastructure.Common
{
    public class ResponseResult
    {
        [JsonProperty("data")]
        public object Data { get; set; }

        [JsonProperty("errorcode")]
        public int ErrorCode { get; set; }

        [JsonProperty("errormsg")]
        public string ErrorMsg { get; set; }

        [JsonProperty("operatetime")]
        public DateTime OperateTime
        {
            get { return DateTime.Now; }
        }

        [JsonProperty("iserror")]
        public bool IsError
        {
            get { return !string.IsNullOrWhiteSpace(ErrorMsg) || ErrorCode != 0; }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new IsoDateTimeConverter { DateTimeFormat = "yyyy/MM/dd HH:mm:ss" });
        }
    }
}
