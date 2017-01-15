using SocialMediaScrapers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Controllers.Source;

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
            //var test = Scraped.InstagramMedia;


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