using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Advanced_Web_Application.helpers
{
    public class IpInfo
    {
        [JsonProperty("ip")]
        public string Ip { get; set; }
        [JsonProperty("hostname")]
        public string HostName { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("region")]
        public string Region { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("loc")]
        public string Location { get; set; }
        [JsonProperty("org")]
        public string Org { get; set; }
        [JsonProperty("timezone")]
        public string TimeZone { get; set; }
        
    }
}
