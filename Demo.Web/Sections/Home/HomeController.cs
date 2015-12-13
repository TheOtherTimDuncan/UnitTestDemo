using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Demo.Web.Sections.Home
{
    [Route(HomeController.Route + "/{action}")]
    public class HomeController : Controller
    {
        public const string Route = "Home";

        [Route("~/")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
