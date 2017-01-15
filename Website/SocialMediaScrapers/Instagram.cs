using Newtonsoft.Json;
using SocialMediaScrapers.Instagram.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using unirest_net.http;

namespace SocialMediaScrapers.Instagram
{
    public class Endpoints
    {
        public const string ACCOUNT_MEDIAS = "https://www.instagram.com/{username}/media?max_id={max_id}&count={count}";
    }

    public class InstagramException : Exception
    {
        public InstagramException() { }
        public InstagramException(string message)
            : base(message) { }
    }

    public partial class Scraper
    {
        public List<Media> GetMedias(string username, int count = 20, string maxId = null)
        {
            string endpoint = Endpoints.ACCOUNT_MEDIAS.Replace("{username}", username).Replace("{count}", count.ToString());

            List<Media> medias = new List<Media>();
            bool isMoreAvailable = true;

            while (isMoreAvailable)
            {
                // pass in maxId, if this is a loop from moreAvailable, the maxId will be the oldest post in the previous pass
                HttpResponse<string> response = Unirest.get(endpoint.Replace("{max_id}", maxId)).asString();

                // if http response is success, we will continue, if not, we cannot do much more so we will just say no more available
                // and return whatever we currently have
                if (response.Code == 200)
                {
                    MediaResponse mediaResponse = JsonConvert.DeserializeObject<MediaResponse>(response.Body);

                    // the instagram response also has a status, if ok continue
                    if (mediaResponse.status == "ok")
                    {
                        medias.AddRange(mediaResponse.items);

                        // Assumption: more_available will be false if there are ever less than specified COUNT items returned
                        // as long as that variable is correctly set, we exit the loop
                        // If there is at least one entry, we will capture the oldest one's id and set that as maxId in case
                        // we need to do another pass in the while loop
                        if (mediaResponse.items.Count() > 0)
                        {
                            maxId = mediaResponse.items.OrderBy(i => i.CreateDate).First().Id;
                        }
                    }

                    isMoreAvailable = mediaResponse.more_available;
                }
                else
                {
                    isMoreAvailable = false;
                }
            }

            return medias;
        }
        


    }
}
