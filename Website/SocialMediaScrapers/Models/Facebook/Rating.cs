using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SocialMediaScrapers.Converters;
using System.Text.RegularExpressions;

namespace SocialMediaScrapers.Facebook.Model
{
    public class Rating
    {
        [JsonProperty("created_time")]
        public DateTime CreateDate { get; set; }

        [JsonProperty("review_text")]
        public string ReviewText { get; set; }

        [JsonProperty("has_rating")]
        public bool HasRating { get; set; }

        [JsonProperty("has_review")]
        public bool HasReview { get; set; }

        [JsonProperty("rating")]
        public int RatingValue { get; set; }

        [JsonProperty("reviewer")]
        public Reviewer Reviewer { get; set; }
    }

    public class Reviewer
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class RatingResponse
    {
        public List<Rating> data { get; set; }
    }
}
