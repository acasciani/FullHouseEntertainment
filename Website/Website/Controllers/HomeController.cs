using SocialMediaScrapers.Photos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Instagram ig = new Instagram()
            {
                SourceID = 1,
                AccessToken = "663558400.bda92d4.fe10e91a8df1423495b80273d25d6f30",
                ScrapedFolder = Server.MapPath("~/Content/images/scraped")
            };

            var test = ig.GetLastScrapedPhoto();
            string maxid = ig.ScrapeAndSave(null);

            while (maxid != null)
            {
                maxid = ig.ScrapeAndSave(maxid);
            }

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