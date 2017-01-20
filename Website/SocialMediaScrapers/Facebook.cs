using Newtonsoft.Json;
using SocialMediaScrapers.Facebook.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unirest_net.http;

namespace SocialMediaScrapers.Facebook
{
    public partial class Scraper
    {
        public class Endpoints
        {
            public const string ACCOUNT_MEDIAS = "https://graph.facebook.com/v2.8/{pageName}/ratings?fields={fields}&access_token={pageAccessToken}";
        }

        public class FacebookException : Exception
        {
            public FacebookException() { }
            public FacebookException(string message)
                : base(message) { }
        }

        public List<Rating> GetRatings(string pageName, string pageAccessToken, int count, List<long> blockedUserIds, int? seedPull = null, int? minimumStars = null)
        {
            // get fields returned
            string fields = "created_time%2Creviewer%2Creview_text%2Chas_rating%2Chas_review%2Crating" + (seedPull.HasValue ? "&limit=" + seedPull.Value.ToString() : "");

            string endpoint = Endpoints.ACCOUNT_MEDIAS.Replace("{pageName}", pageName).Replace("{fields}", fields).Replace("{pageAccessToken}", pageAccessToken);

            List<Rating> ratings = new List<Rating>();

            HttpResponse<string> response = Unirest.get(endpoint).asString();

            // if http response is success, we will continue, if not, we cannot do much more so we will just say no more available
            // and return whatever we currently have
            if (response.Code == 200)
            {
                RatingResponse ratingResponse = JsonConvert.DeserializeObject<RatingResponse>(response.Body);

                if (ratingResponse.data != null)
                {
                    // get data from unblocked FB users only
                    var relevantData = ratingResponse.data.Where(i => i.HasRating && i.HasReview).Where(i => i.Reviewer != null && !blockedUserIds.Contains(i.Reviewer.Id));

                    if (minimumStars.HasValue)
                    {
                        return relevantData.Where(i => i.RatingValue >= minimumStars.Value).OrderByDescending(i => i.CreateDate).Take(count).ToList();
                    }
                    else
                    {
                        return relevantData.OrderByDescending(i => i.CreateDate).Take(count).ToList();
                    }
                }
            }

            return ratings;
        }
    }
}
