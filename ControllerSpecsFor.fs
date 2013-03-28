namespace SpecsForExtensions

open System
open System.Security.Principal
open System.Web
open System.Web.Mvc
open System.Web.Routing
open SpecsFor

type ControllerSpecsFor<'a when 'a : not struct and 'a :> Controller>() =
    inherit SpecsFor<'a>()

    override this.InitializeClassUnderTest() =
        base.InitializeClassUnderTest()

        let context = this.GetMockFor<HttpContextBase>()
        let session = this.GetMockFor<HttpSessionStateBase>()
        let request = this.GetMockFor<HttpRequestBase>()
        let response = this.GetMockFor<HttpResponseBase>()
        let server = this.GetMockFor<HttpServerUtilityBase>()
        let user = this.GetMockFor<IPrincipal>()
        let identity = this.GetMockFor<IIdentity>()
        let uri = new Uri("http://localhost:1234")
        let requestContext = new RequestContext(context.Object, new RouteData())

        context.Setup<_>(fun x -> x.Session).Returns(session.Object) |> ignore
        context.Setup<_>(fun x -> x.Server).Returns(server.Object) |> ignore
        context.Setup<_>(fun x -> x.Request).Returns(request.Object) |> ignore
        context.Setup<_>(fun x -> x.Response).Returns(response.Object) |> ignore
        context.Setup<_>(fun x -> x.User).Returns(user.Object) |> ignore
        request.Setup<_>(fun x -> x.Url).Returns(uri) |> ignore
        user.Setup<_>(fun x -> x.Identity).Returns(identity.Object) |> ignore

        this.SUT.ControllerContext <- new ControllerContext (context.Object, new RouteData(), this.SUT)
        this.SUT.Url <- new UrlHelper(requestContext)
