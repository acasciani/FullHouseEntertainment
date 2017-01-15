using SocialMediaScrapers;
using SocialMediaScrapers.Instagram.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Controllers.Source;
using Website.Models;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        private static Media _WeddingCoverImage;
        private static Media _PhotoBoothCoverImage;
        private static Media _CorporatePartiesCoverImage;
        private static Media _OtherServicesCoverImage;

        private static DateTime? _StaticUpdates;

        public ActionResult Index()
        {
            if (!_StaticUpdates.HasValue || DateTime.UtcNow < _StaticUpdates.Value)
            {
                _WeddingCoverImage = null;
                _PhotoBoothCoverImage = null;
                _CorporatePartiesCoverImage = null;
                _OtherServicesCoverImage = null;
                _StaticUpdates = DateTime.UtcNow.AddHours(6);
            }

            if (_PhotoBoothCoverImage == null)
            {
                var scrapedCovers = new InstagramGallery(ServicesController.WeddingTag, ServicesController.CoverImageTag);
                _WeddingCoverImage = scrapedCovers.MediaItems.Where(i => i.MediaType == MediaType.Image).OrderByDescending(i => i.CreateDate).FirstOrDefault();
            }

            if (_PhotoBoothCoverImage == null)
            {
                var scrapedCovers = new InstagramGallery(ServicesController.PhotoBoothTag, ServicesController.CoverImageTag);
                _PhotoBoothCoverImage = scrapedCovers.MediaItems.Where(i => i.MediaType == MediaType.Image).OrderByDescending(i => i.CreateDate).FirstOrDefault();
            }

            if (_CorporatePartiesCoverImage == null)
            {
                var scrapedCovers = new InstagramGallery(ServicesController.CorporatePartyTag, ServicesController.CoverImageTag);
                _CorporatePartiesCoverImage = scrapedCovers.MediaItems.Where(i => i.MediaType == MediaType.Image).OrderByDescending(i => i.CreateDate).FirstOrDefault();
            }

            if (_OtherServicesCoverImage == null)
            {
                var scrapedCovers = new InstagramGallery(ServicesController.OtherServicesTag, ServicesController.CoverImageTag);
                _OtherServicesCoverImage = scrapedCovers.MediaItems.Where(i => i.MediaType == MediaType.Image).OrderByDescending(i => i.CreateDate).FirstOrDefault();
            }

            ViewBag.WeddingCoverImage = _WeddingCoverImage;
            ViewBag.PhotoBoothCoverImage = _PhotoBoothCoverImage;
            ViewBag.CorporatePartiesCoverImage = _CorporatePartiesCoverImage;
            ViewBag.OtherServicesCoverImage = _OtherServicesCoverImage;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}