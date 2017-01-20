using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Website.Controllers.Source;
using Website.Controllers.Source.Scrapers;

namespace Website.Controllers
{
    public class ManagementController : Controller
    {
        // GET: Management
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Ratings()
        {
            return View(Scraped.FacebookRatings);
        }

        public ActionResult BlockFBUser(long id)
        {
            // block user
            FacebookScraperJob fbscraper = new FacebookScraperJob();

            // try three times, then error out
            for (int i = 0; i < 3; i++)
            {
                if (fbscraper.BlockFBUserId(id))
                {
                    // success! clear cached ratings
                    Scraped.RemoveFBCache();
                    return RedirectToAction("Ratings");
                }
            }

            // unable to delete.
            return RedirectToAction("Ratings", new { errorMsg = "Unable to hide rating. Try again." });
        }
    }
}