using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SocialMediaScrapers.Instagram.Model
{
    public enum MediaType{Image, Video}
    
    
    public class Media
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_time")]
        [JsonConverter(typeof(MicrosecondEpochConverter))]
        public DateTime CreateDate { get; set; }
    }

    public class MediaResponse
    {
        [JsonProperty("items")]
        public List<Media> items { get; set; }
    }
}
