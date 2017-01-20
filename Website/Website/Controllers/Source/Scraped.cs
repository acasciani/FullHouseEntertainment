using SocialMediaScrapers.Facebook.Model;
using SocialMediaScrapers.Instagram.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Controllers.Source.Scrapers;

namespace Website.Controllers.Source
{
    public static class Scraped
    {
        public static List<Media> InstagramMedia
        {
            get
            {
                List<Media> medias = HttpRuntime.Cache["Scraped_InstagramMedia"] as List<Media>;
                if (medias == null)
                {
                    InstagramScraperJob job = new InstagramScraperJob();
                    UpdateIGCache(job.GetMedias());
                    medias = HttpRuntime.Cache["Scraped_InstagramMedia"] as List<Media>;
                }

                return medias;
            }
            set
            {
                UpdateIGCache(value);
            }
        }

        private static void UpdateIGCache(List<Media> medias)
        {
            HttpRuntime.Cache.Insert("Scraped_InstagramMedia", medias, null, DateTime.MaxValue, System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, null);
        }

        public static List<Rating> FacebookRatings
        {
            get
            {
                List<Rating> ratings = HttpRuntime.Cache["Scraped_FacebookRatings"] as List<Rating>;
                if (ratings == null)
                {
                    FacebookScraperJob job = new FacebookScraperJob();
                    UpdateFBCache(job.GetRatings());
                    ratings = HttpRuntime.Cache["Scraped_FacebookRatings"] as List<Rating>;
                }

                return ratings;
            }
            set
            {
                UpdateFBCache(value);
            }
        }

        public static void RemoveFBCache()
        {
            HttpRuntime.Cache.Remove("Scraped_FacebookRatings");
        }

        private static void UpdateFBCache(List<Rating> ratings)
        {
            HttpRuntime.Cache.Insert("Scraped_FacebookRatings", ratings, null, DateTime.MaxValue, System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, null);
        }
    }
}