using Newtonsoft.Json;
using SocialMediaScrapers.Instagram.Model;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialMediaScrapers.Converters.Instagram
{
    public class TagConverter : JsonConverter
    {
        public override bool CanConvert(System.Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, System.Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) { return null; }

            string value = reader.Value.ToString();

            // get hashtags
            var regex = new Regex(@"(?<=#)\w+");
            var matches = regex.Matches(value);

            List<string> tags = new List<string>();

            foreach (Match m in matches)
            {
                tags.Add(m.Value);
            }

            return tags;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // do nothing
        }
    }
}
