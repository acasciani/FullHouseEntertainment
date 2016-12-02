using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Deserializers;
using RestSharp;
using System.IO;
using PhotosDAL;

namespace SocialMediaScrapers.Photos
{
    public partial class Instagram
    {
        public class IGImage
        {
            [DeserializeAs(Attribute = true, Name = "url")]
            public string URL { get; set; }
        }

        public class IGImages
        {
            [DeserializeAs(Attribute = true, Name = "low_resolution")]
            public IGImage LowResolution { get; set; }

            [DeserializeAs(Attribute = true, Name = "thumbnail")]
            public IGImage Thumbnail { get; set; }

            [DeserializeAs(Attribute = true, Name = "standard_resolution")]
            public IGImage StandardResolution { get; set; }
        }

        public class IGElementData
        {
            [DeserializeAs(Attribute = true, Name = "type")]
            public string MediaType { get; set; }

            [DeserializeAs(Attribute = true, Name = "created_time")]
            public DateTime DateCreated { get; set; }

            [DeserializeAs(Attribute = true, Name = "images")]
            public IGImages Images { get; set; }

            [DeserializeAs(Attribute = true, Name = "id")]
            public string MediaID { get; set; }
        }

        public class IGElement
        {
            [DeserializeAs(Attribute = true, Name = "data")]
            public List<IGElementData> Data { get; set; }
        }

        public int SourceID { get; set; }
        public string AccessToken { get; set; }
        public string ScrapedFolder { get; set; }
        private string Folder { get { return ScrapedFolder + "/instagram"; } }
        private const int Count = 100;

        public string GetLastScrapedPhoto()
        {
            PhotosDAL.Photo lastPhoto = null;
            using (PhotosDAL.PhotosModel pm = new PhotosDAL.PhotosModel())
            {
                lastPhoto = pm.Photos.Where(i => i.SourceID == SourceID).OrderByDescending(i => i.PhotoDate).FirstOrDefault();
            }

            // returns the IG photo id
            return lastPhoto == null ? null : lastPhoto.SourcePhotoID;
        }

        /// <summary>
        /// Returns true when the number of scraped and saved is less than 20. Throws an exception if unable to scrape and save more one or more files.
        /// </summary>
        public string ScrapeAndSave(string maxID)
        {
            Directory.CreateDirectory(Folder);

            var client = new RestClient("https://api.instagram.com/v1");

            var request = new RestRequest("users/self/media/recent/", Method.GET);
            request.AddParameter("access_token", AccessToken);
            request.AddParameter("max_id", maxID);
            request.AddParameter("count", Count);

            IRestResponse<IGElement> response2 = client.Execute<IGElement>(request);

            List<string> returnedIGids = response2.Data.Data.Select(i => i.MediaID).ToList();
            List<string> alreadyUploaded = null;
            using (PhotosModel pm = new PhotosModel())
            {
                alreadyUploaded = pm.Photos.Where(i => returnedIGids.Contains(i.SourcePhotoID)).Select(i => i.SourcePhotoID).ToList();
            }

            var itemsToUploadOrdered = response2.Data.Data.Where(i => !alreadyUploaded.Contains(i.MediaID)).OrderByDescending(i => i.DateCreated);

            Parallel.ForEach<IGElementData>(itemsToUploadOrdered, item => SaveAndUpload(item));


            return itemsToUploadOrdered.Count() < Count ? null : itemsToUploadOrdered.Last().MediaID;
        }


        public void SaveAndUpload(IGElementData igElement)
        {

            var client = new RestClient(igElement.Images.StandardResolution.URL);
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);
            var content = response.Content;

            Photo photo = new Photo()
            {
                Captured = DateTime.UtcNow,
                PhotoDate = igElement.DateCreated,
                SourceID = SourceID,
                SourcePhotoID = igElement.MediaID
            };

            int insertedID;
            using (PhotosModel pm = new PhotosModel())
            {
                pm.Add(photo);
                pm.SaveChanges();
                insertedID = photo.PhotoID;
            }

            // save to file system
            using (FileStream fs = new FileStream(Folder + "/" + insertedID + "." + GetExtension(response.ContentType), FileMode.Create, FileAccess.Write))
            {
                fs.Write(response.RawBytes, 0, response.RawBytes.Length);
            }

            using (PhotosModel pm = new PhotosModel())
            {
                Photo updatePhoto = pm.Photos.First(i => i.PhotoID == insertedID);
                updatePhoto.LocalPath = "instagram" + "/" + insertedID + "." + GetExtension(response.ContentType);
                pm.SaveChanges();
            }
        }


        public string GetExtension(string contentType)
        {
            switch (contentType)
            {
                case "image/jpeg": return "jpg";
                case "image/jpg": return "jpg";
                case "image/png": return "png";
                case "image/gif": return "gif";
                default: return "jpg";
            }
        }
    }
}
