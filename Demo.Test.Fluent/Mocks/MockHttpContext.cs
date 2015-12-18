using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Moq;

namespace Demo.Test.Fluent.Mocks
{
    public class MockHttpContext : Mock<HttpContextBase>
    {
        public MockHttpContext()
        {
            HttpCookieCollection cookies = new HttpCookieCollection();

            Mock<HttpRequestBase> mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(x => x.ApplicationPath).Returns(WebAppPath);
            mockRequest.Setup(x => x.Url).Returns(new Uri("http://localhost/debug"));

            Mock<HttpResponseBase> mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(x => x.ApplyAppPathModifier(It.IsAny<string>())).Returns((string s) => s);

            this.Setup(x => x.Request).Returns(mockRequest.Object);
            this.Setup(x => x.Response).Returns(mockResponse.Object);

            RequestContext requestContext = new RequestContext(this.Object, new RouteData());
            mockRequest.Setup(x => x.RequestContext).Returns(requestContext);
        }

        public string WebAppPath
        {
            get
            {
                return "/debug/";
            }
        }
    }
}
