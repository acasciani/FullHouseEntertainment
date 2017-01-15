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
    }
}