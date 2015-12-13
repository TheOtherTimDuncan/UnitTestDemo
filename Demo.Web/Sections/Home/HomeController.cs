using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Demo.Web.Domain.Contracts.Commands;
using Demo.Web.Domain.Contracts.Queries;
using Demo.Web.Domain.Services.Commands;
using Demo.Web.Sections.Common;
using Demo.Web.Sections.Home.Models;
using Demo.Web.Sections.Home.Queries;

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
        public async Task<ActionResult> Index(BlogQuery query)
        {
            BlogModel model = await QueryDispatcher.DispatchAsync(query);
            return View(model);
        }

        [HttpPost]
        [Route("~/")]
        public async Task<ActionResult> Index(BlogModel model)
        {
            if (ModelState.IsValid)
            {
                CommandResult commandResult = await CommandDispatcher.DispatchAsync(model);
                if (commandResult.Succeeded)
                {
                    return RedirectToAction("Index", new BlogQuery()
                    {
                        BlogID = model.ID
                    });
                }

                ModelState.AddModelError("", string.Join(", ", commandResult.Errors));
            }

            return View(model);
        }
    }
}
