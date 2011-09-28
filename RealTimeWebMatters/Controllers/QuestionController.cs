using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using PusherRESTDotNet;

namespace RealTimeWebMatters.Controllers
{
    public class QuestionController : Controller
    {
        //
        // GET: /Question/

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Ask(string asker, string question)
        {
            var applicationId = ConfigurationManager.AppSettings.Get("pusher-app-id");
            var applicationKey = ConfigurationManager.AppSettings.Get("pusher-app-key");
            var applicationSecret = ConfigurationManager.AppSettings.Get("pusher-app-secret");
            var provider = new PusherProvider(applicationId, applicationKey, applicationSecret);
            var request = new ObjectPusherRequest("questions-why_the_realtimeweb_matters", "new_question", new
            {
                asker = asker,
                question = question
            });

            provider.Trigger(request);

            TempData["Message"] = "Thanks for your question, " + asker;

            return RedirectToAction("Index");
        }
    }
}
