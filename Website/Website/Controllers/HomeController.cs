using SocialMediaScrapers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Website.Controllers
{
    public static class LastScraped
    {
        public static string ID { get; set; }
    }

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            SocialMediaScrapers.Instagram.Scraper ig = new SocialMediaScrapers.Instagram.Scraper();
            ig.GetMedias("cashciani");


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