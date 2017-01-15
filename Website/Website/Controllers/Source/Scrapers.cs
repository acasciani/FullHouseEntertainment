using Quartz;
using SocialMediaScrapers.Instagram;
using SocialMediaScrapers.Instagram.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Website.Controllers.Source.Scrapers
{
    public class InstagramScraperJob : IJob
    {
        private static string InstagramUsername = null;

        public List<Media> GetMedias()
        {
            if (InstagramUsername == null)
            {
                InstagramUsername = ConfigurationManager.AppSettings["InstagramUsername"];
            }

            Scraper scraper = new Scraper();
            return scraper.GetMedias(InstagramUsername);
        }

        public void Execute(IJobExecutionContext context)
        {
            List<Media> medias = GetMedias();
            Scraped.InstagramMedia = medias;
        }
    }
}