namespace SpecsForExtensions
{
    using System;
    using System.Security.Principal;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class ControllerSpecsFor<T> : DbSpecsFor<T>
        where T : Controller
    {
        protected ControllerContext ControllerContext;

        protected override void InitializeClassUnderTest ()
        {
            base.InitializeClassUnderTest ();

            var context = GetMockFor<HttpContextBase> ();
            var session = GetMockFor<HttpSessionStateBase> ();
            var request = GetMockFor<HttpRequestBase> ();
            var response = GetMockFor<HttpResponseBase> ();
            var server = GetMockFor<HttpServerUtilityBase> ();
            var user = GetMockFor<IPrincipal> ();
            var identity = GetMockFor<IIdentity> ();
            var uri = new Uri ("http://localhost:12345");
            var requestContext = new RequestContext (context.Object, new RouteData ());

            context.Setup (x => x.Session).Returns (session.Object);
            context.Setup (x => x.Server).Returns (server.Object);
            context.Setup (x => x.Request).Returns (request.Object);
            context.Setup (x => x.Response).Returns (response.Object);
            context.Setup (x => x.User).Returns (user.Object);
            request.Setup (x => x.Url).Returns (uri);
            user.Setup (x => x.Identity).Returns (identity.Object);

            SUT.ControllerContext = ControllerContext = new ControllerContext (context.Object, new RouteData (), SUT);
            SUT.Url = new UrlHelper (requestContext);
        }
    }
}