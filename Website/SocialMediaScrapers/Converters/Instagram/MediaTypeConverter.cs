using Newtonsoft.Json;
using SocialMediaScrapers.Instagram.Model;

namespace SocialMediaScrapers.Converters.Instagram
{
    public class MediaTypeConverter : JsonConverter
    {
        public override bool CanConvert(System.Type objectType)
        {
            return objectType == typeof(string) || objectType == typeof(MediaType);
        }

        public override object ReadJson(JsonReader reader, System.Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) { return null; }

            if (reader.ValueType == typeof(string))
            {
                switch ((string)reader.Value)
                {
                    case "image": return MediaType.Image;
                    case "video": return MediaType.Video;
                    default: return MediaType.Unknown;
                }
            }
            else
            {
                return reader.Value;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            MediaType type = (MediaType)value;

            switch (type)
            {
                case MediaType.Video: writer.WriteRawValue("video"); break;
                case MediaType.Image: writer.WriteRawValue("image"); break;
                case MediaType.Unknown: writer.WriteRawValue("unknown"); break;
                default: writer.WriteRawValue(""); break;
            }
        }
    }
}
