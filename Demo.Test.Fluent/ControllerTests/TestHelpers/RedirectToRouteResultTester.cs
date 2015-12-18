using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using System.Web.Routing;

namespace Demo.Test.Fluent.ControllerTests.TestHelpers
{
    public class RedirectToRouteResultTester
    {
        private RedirectToRouteResult _redirectResult;

        public RedirectToRouteResultTester(RedirectToRouteResult redirectResult)
        {
            this._redirectResult = redirectResult;
        }

        public RedirectToRouteResultTester HavingControllerRoute<ControllerType>()
        {
            string routeName = typeof(ControllerType).Name;
            routeName = routeName.Substring(0, routeName.Length - "Controller".Length);
            return HavingControllerRoute(routeName);
        }

        public RedirectToRouteResultTester HavingControllerRoute(string route)
        {
            _redirectResult.RouteValues["controller"].Should().Be(route);
            return this;
        }

        public RedirectToRouteResultTester HavingActionRoute(string actionName)
        {
            _redirectResult.RouteValues["action"].Should().Be(actionName);
            return this;
        }

        public RedirectToRouteResultTester HavingRouteValues(object routeValues)
        {
            RouteValueDictionary testValues = new RouteValueDictionary(routeValues);
            foreach (var keyValue in testValues)
            {
                _redirectResult.RouteValues.Should().Contain(keyValue);
            }
            return this;
        }
    }
}
