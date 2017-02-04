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
        private static Media _PrivatePartiesCoverImage;
        private static Media _OtherServicesCoverImage;

        private static InstagramGallery _WeddingGallery;
        private static InstagramGallery _PhotoBoothGallery;
        private static InstagramGallery _PrivatePartiesGallery;
        private static InstagramGallery _OtherServicesGallery;

        private static List<SocialMediaScrapers.Facebook.Model.Rating> _FacebookRatings;

        private static DateTime? _StaticUpdates;

        public ActionResult Index()
        {
            if (!_StaticUpdates.HasValue || DateTime.UtcNow < _StaticUpdates.Value)
            {
                _FacebookRatings=null;
                _WeddingCoverImage = null;
                _PhotoBoothCoverImage = null;
                _PrivatePartiesCoverImage = null;
                _OtherServicesCoverImage = null;
                _WeddingGallery = null;
                _PhotoBoothGallery = null;
                _PrivatePartiesGallery = null;
                _OtherServicesGallery = null;
                _StaticUpdates = DateTime.UtcNow.AddHours(6);
            }

            if (_WeddingCoverImage == null)
            {
                var scrapedCovers = new InstagramGallery(1, ServicesController.WeddingTag, ServicesController.CoverImageTag);
                _WeddingCoverImage = scrapedCovers.MediaItems.Where(i => i.MediaType == MediaType.Image).OrderByDescending(i => i.CreateDate).FirstOrDefault();
            }

            if (_PhotoBoothCoverImage == null)
            {
                var scrapedCovers = new InstagramGallery(1, ServicesController.PhotoBoothTag, ServicesController.CoverImageTag);
                _PhotoBoothCoverImage = scrapedCovers.MediaItems.Where(i => i.MediaType == MediaType.Image).OrderByDescending(i => i.CreateDate).FirstOrDefault();
            }

            if (_PrivatePartiesCoverImage == null)
            {
                var scrapedCovers = new InstagramGallery(1, ServicesController.PrivatePartyTag, ServicesController.CoverImageTag);
                _PrivatePartiesCoverImage = scrapedCovers.MediaItems.Where(i => i.MediaType == MediaType.Image).OrderByDescending(i => i.CreateDate).FirstOrDefault();
            }

            if (_OtherServicesCoverImage == null)
            {
                var scrapedCovers = new InstagramGallery(1, ServicesController.OtherServicesTag, ServicesController.CoverImageTag);
                _OtherServicesCoverImage = scrapedCovers.MediaItems.Where(i => i.MediaType == MediaType.Image).OrderByDescending(i => i.CreateDate).FirstOrDefault();
            }

            if (_WeddingGallery == null)
            {
                _WeddingGallery = new InstagramGallery(10, ServicesController.WeddingTag);
                _WeddingGallery.ShowOnlyFirstLink = true;
            }
            if (_PhotoBoothGallery == null)
            {
                _PhotoBoothGallery = new InstagramGallery(10, ServicesController.PhotoBoothTag);
                _PhotoBoothGallery.ShowOnlyFirstLink = true;
            }
            if (_PrivatePartiesGallery == null)
            {
                _PrivatePartiesGallery = new InstagramGallery(10, ServicesController.PrivatePartyTag);
                _PrivatePartiesGallery.ShowOnlyFirstLink = true;
            }
            if (_OtherServicesGallery == null)
            {
                _OtherServicesGallery = new InstagramGallery(10, ServicesController.OtherServicesTag);
                _OtherServicesGallery.ShowOnlyFirstLink = true;
            }

            ViewBag.WeddingCoverImage = _WeddingCoverImage;
            ViewBag.PhotoBoothCoverImage = _PhotoBoothCoverImage;
            ViewBag.PrivatePartiesCoverImage = _PrivatePartiesCoverImage;
            ViewBag.OtherServicesCoverImage = _OtherServicesCoverImage;

            ViewBag.WeddingGallery = _WeddingGallery;
            ViewBag.PhotoBoothGallery = _PhotoBoothGallery;
            ViewBag.PrivatePartiesGallery = _PrivatePartiesGallery;
            ViewBag.OtherServicesGallery = _OtherServicesGallery;

            // facebook ratings
            if (_FacebookRatings == null)
            {
                _FacebookRatings = Scraped.FacebookRatings;
            }

            ViewBag.FacebookRatings = _FacebookRatings;

            return View();
        }

        public ActionResult Awards()
        {
            return View();
        }

        public ActionResult Affiliates()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}