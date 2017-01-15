using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SocialMediaScrapers.Converters;
using SocialMediaScrapers.Converters.Instagram;
using System.Text.RegularExpressions;

namespace SocialMediaScrapers.Instagram.Model
{
    public enum MediaType{Image, Video, Unknown}
    
    public class Media
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_time")]
        [JsonConverter(typeof(MicrosecondEpochConverter))]
        public DateTime CreateDate { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(MediaTypeConverter))]
        public MediaType MediaType { get; set; }

        [JsonProperty("images")]
        public Images Images { get; set; }

        [JsonProperty("caption")]
        public Caption Caption { get; set; }
    }

    public class Images
    {
        [JsonProperty("standard_resolution")]
        public Image StandardResolution { get; set; }

        [JsonProperty("thumbnail")]
        public Image Thumbnail { get; set; }

        [JsonProperty("low_resolution")]
        public Image LowResolution { get; set; }
    }

    public class Image
    {
        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class Caption
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        public List<string> Tags {
            get
            {
                // get hashtags
                var regex = new Regex(@"(?<=#)\w+");
                var matches = regex.Matches(Text);

                List<string> tags = new List<string>();

                foreach (Match m in matches)
                {
                    tags.Add(m.Value);
                }

                return tags;
            }
        }
    }

    public class MediaResponse
    {
        public List<Media> items { get; set; }

        public string status { get; set; }

        public bool more_available { get; set; }
    }
}
