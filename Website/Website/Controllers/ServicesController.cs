using SocialMediaScrapers.Instagram.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Controllers.Source;
using Website.Models;

namespace Website.Controllers
{
    public class ServicesController : Controller
    {
        public static string CoverImageTag = ConfigurationManager.AppSettings["CoverImageTag"];
        public static string WeddingTag = ConfigurationManager.AppSettings["WeddingTag"];
        public static string PhotoBoothTag = ConfigurationManager.AppSettings["PhotoBoothTag"];
        public static string CorporatePartyTag = ConfigurationManager.AppSettings["CorporatePartyTag"];
        public static string OtherServicesTag = ConfigurationManager.AppSettings["OtherServicesTag"];

        private InstagramGallery _WeddingGallery; // work to make static?
        private InstagramGallery _WeddingCover;
        public ActionResult Weddings()
        {
            if (_WeddingGallery == null)
            {
                _WeddingGallery = new InstagramGallery(50, new string[] { WeddingTag });
            }

            if (_WeddingCover == null)
            {
                _WeddingCover = new InstagramGallery(1, new string[] { WeddingTag, CoverImageTag });
            }

            ViewBag.Gallery = _WeddingGallery;
            ViewBag.CoverImage = _WeddingCover; // if we want hardcoded, this needs to be removed

            return View();
        }

        private InstagramGallery _PhotoBoothGallery;
        private InstagramGallery _PhotoBoothCover;
        public ActionResult PhotoBooth()
        {
            if (_PhotoBoothGallery == null)
            {
                _PhotoBoothGallery = new InstagramGallery(50, new string[] { PhotoBoothTag });
            }

            if (_PhotoBoothCover == null)
            {
                _PhotoBoothCover = new InstagramGallery(1, new string[] { PhotoBoothTag, CoverImageTag });
            }

            ViewBag.Gallery = _PhotoBoothGallery;
            ViewBag.CoverImage = _PhotoBoothCover;

            return View();
        }

        private InstagramGallery _CorporatePartiesGallery;
        private InstagramGallery _CorporatePartiesCover;
        public ActionResult CorporateParties()
        {
            if (_CorporatePartiesGallery == null)
            {
                _CorporatePartiesGallery = new InstagramGallery(50, new string[] { CorporatePartyTag });
            }

            if (_CorporatePartiesCover == null)
            {
                _CorporatePartiesCover = new InstagramGallery(1, new string[] { CorporatePartyTag, CoverImageTag });
            }

            ViewBag.Gallery = _CorporatePartiesGallery;
            ViewBag.CoverImage = _CorporatePartiesCover;

            return View();
        }

        private InstagramGallery _OtherServicesGallery;
        private InstagramGallery _OtherServicesCover;
        public ActionResult OtherServices()
        {
            if (_OtherServicesGallery == null)
            {
                _OtherServicesGallery = new InstagramGallery(50, new string[] { OtherServicesTag });
            }

            if (_OtherServicesCover == null)
            {
                _OtherServicesCover = new InstagramGallery(1, new string[] { OtherServicesTag, CoverImageTag });
            }

            ViewBag.Gallery = _OtherServicesGallery;
            ViewBag.CoverImage = _OtherServicesCover;

            return View();
        }
    }
}