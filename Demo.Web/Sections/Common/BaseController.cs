using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Demo.Web.Domain.Contracts.Commands;
using Demo.Web.Domain.Contracts.Queries;

namespace Demo.Web.Sections.Common
{
    public class BaseController : Controller
    {
        private IQueryDispatcher _queryDispatcher;
        private ICommandDispatcher _commandDispatcher;

        public BaseController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            this._queryDispatcher = queryDispatcher;
            this._commandDispatcher = commandDispatcher;
        }

        protected IQueryDispatcher QueryDispatcher
        {
            get
            {
                return _queryDispatcher;
            }
        }

        protected ICommandDispatcher CommandDispatcher
        {
            get
            {
                return _commandDispatcher;
            }
        }
    }
}
