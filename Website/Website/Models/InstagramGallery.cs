using SocialMediaScrapers.Instagram.Model;
using System.Collections.Generic;
using System.Linq;
using Website.Controllers.Source;

namespace Website.Models
{
    public class InstagramGallery
    {
        public InstagramGallery(params string[] tags)
        {
            List<string> tagsClean = tags.Select(i => i.ToLower().Trim()).Distinct().ToList();

            var instagramMedia = Scraped.InstagramMedia;
            if (instagramMedia != null)
            {
                if (tags.Count() == 0)
                {
                    // return all
                    MediaItems = instagramMedia.OrderByDescending(i => i.CreateDate).Take(50).ToList();
                }
                else
                {
                    var items = instagramMedia
                        .Where(i => i.Caption != null && i.Caption.Tags != null)
                        .Where(i => i.Caption.Tags.Select(j => j.Trim().ToLower()).ToList().Intersect(tagsClean).Count() == tagsClean.Count())
                        .OrderByDescending(i => i.CreateDate)
                        .Take(50)
                        .ToList();

                    MediaItems = items;
                }
            }
            else
            {
                MediaItems = new List<Media>();
            }

            DataLightboxElement = string.Join("|", tagsClean);
        }

        public List<Media> MediaItems { get; private set; }
        public string DataLightboxElement { get; private set; }
    }
}