using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Demo.Web.Domain.Contracts.Commands;
using Demo.Web.Domain.Contracts.Queries;
using Demo.Web.Sections.Common;

namespace Demo.Web.Sections.Home
{
    [Route(HomeController.Route + "/{action}")]
    public class HomeController : BaseController
    {
        public const string Route = "Home";

        public HomeController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
          : base(queryDispatcher, commandDispatcher)
        {
        }

        [Route("~/")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
