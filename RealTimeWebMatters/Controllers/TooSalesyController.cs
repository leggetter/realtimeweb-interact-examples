using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using PusherRESTDotNet;

namespace RealTimeWebMatters.Controllers
{
    public class TooSalesyController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Vote()
        {
            var applicationId = ConfigurationManager.AppSettings.Get("pusher-app-id");
            var applicationKey = ConfigurationManager.AppSettings.Get("pusher-app-key");
            var applicationSecret = ConfigurationManager.AppSettings.Get("pusher-app-secret");
            var provider = new PusherProvider(applicationId, applicationKey, applicationSecret);
            int downAmount = 5;
            var request = new ObjectPusherRequest("salesy-why_the_realtimeweb_matters", "new_vote", new
            {
                amount = downAmount
            });

            provider.Trigger(request);

            TempData["Message"] = "A bit harsh, but you've voted Phil down by " + downAmount + " points.";

            return RedirectToAction("Index");
        }
    }
}
