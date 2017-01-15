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
                _WeddingGallery = new InstagramGallery(new string[] { WeddingTag });
            }

            if (_WeddingCover == null)
            {
                _WeddingCover = new InstagramGallery(new string[] { WeddingTag, CoverImageTag });
            }

            ViewBag.Gallery = _WeddingGallery;
            ViewBag.CoverImage = _WeddingCover; // if we want hardcoded, this needs to be removed

            return View();
        }
    }
}