using Newtonsoft.Json;
using Quartz;
using SocialMediaScrapers.Facebook;
using SocialMediaScrapers.Facebook.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace Website.Controllers.Source.Scrapers
{
    public class FacebookScraperJob : IJob
    {
        private static string FacebookPageAccessToken = null;
        private static string FacebookPageId = null;
        private static string _FacebookReviewsMinimumStars = null;
        private static string _FacebookReviewsCount = null;

        private int? FacebookReviewsMinimumStars
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_FacebookReviewsMinimumStars)) { return null; }
                return int.Parse(_FacebookReviewsMinimumStars);
            }
        }
        private int FacebookReviewsCount
        {
            get
            {
                return int.Parse(_FacebookReviewsCount);
            }
        }

        public List<Rating> GetRatings()
        {
            if (FacebookPageAccessToken == null)
            {
                FacebookPageAccessToken = ConfigurationManager.AppSettings["FacebookPageAccessToken"];
            }

            if (FacebookPageId == null)
            {
                FacebookPageId = ConfigurationManager.AppSettings["FacebookPageId"];
            }

            if (_FacebookReviewsMinimumStars == null)
            {
                _FacebookReviewsMinimumStars = ConfigurationManager.AppSettings["FacebookReviewsMinimumStars"];
            }

            if (_FacebookReviewsCount == null)
            {
                _FacebookReviewsCount = ConfigurationManager.AppSettings["FacebookReviewsCount"];
            }

            Scraper scraper = new Scraper();
            return scraper.GetRatings(FacebookPageId, FacebookPageAccessToken, FacebookReviewsCount, GetBlockedFBUserIds(), null, FacebookReviewsMinimumStars);
        }

        public void Execute(IJobExecutionContext context)
        {
            List<Rating> ratings = GetRatings();
            Scraped.FacebookRatings = ratings;
        }

        private List<long> GetBlockedFBUserIds()
        {
            string path = HttpContext.Current.Server.MapPath("~/App_Data/BlockedFBUsers.json");
            string currentBlockedUserIds = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<long>>(currentBlockedUserIds) ?? new List<long>();
        }

        public bool BlockFBUserId(long userId)
        {
            string path = HttpContext.Current.Server.MapPath("~/App_Data/BlockedFBUsers.json");
            
            DateTime operationStarted = DateTime.UtcNow;

            List<long> listOfIds;

            if (File.Exists(path))
            {
                string currentBlockedUserIds = File.ReadAllText(path);
                listOfIds = string.IsNullOrWhiteSpace(currentBlockedUserIds) ? new List<long>() : JsonConvert.DeserializeObject<List<long>>(currentBlockedUserIds);
            }
            else
            {
                listOfIds = new List<long>();
            }

            listOfIds.Add(userId);

            try
            {
                using (FileStream fs = File.OpenWrite(path))
                using(StreamWriter writer = new StreamWriter(fs))
                {
                    if (File.GetLastWriteTimeUtc(path) > operationStarted)
                    {
                        return false;
                    }
                    else
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(writer, listOfIds.Distinct().ToList());
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}