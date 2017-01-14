using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using PhotosDAL;
using SocialMediaScrapers.Instagram.Model;
using System.Net;
using Newtonsoft.Json;
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
            string endpoint = Endpoints.ACCOUNT_MEDIAS.Replace("{username}", username).Replace("{max_id}", maxId).Replace("{count}", count.ToString());

            List<Media> medias = new List<Media>();
            bool isMoreAvailable = true;

            while (isMoreAvailable)
            {
                var request = Unirest.get(endpoint);
                var test = request.asString();

                var test2 = JsonConvert.DeserializeObject<MediaResponse>(test.Body);

                isMoreAvailable = false;
            }

            return medias;
        }
        


    }
}
