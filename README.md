scriptcs-webapi-owin
====================

# ASP.Net Webapi Owin Script Pack

## What is it?
Allows ASP.Net WebApi to be included as a Owin component with scriptcs

## Highlights:

* Based on pre-release ASP.Net 2.0 daily build.
* Includes dependancies for new ASP.Net 2.0 features (e.g. Routes, CORS, OData)

## Getting started with ASP.Net Web Api using the pack

```csharp
[Route("api/test")]
public class TestController : ApiController {
	public string Get { 
		return "Hello World";
	}
}

var webApi =  Require<WebApiOwin>();
webApi.Configuration.MapHttpAttributeRoutes();

var owin = Require<OwinSelfHost>();
using ( webApi.CreateServer("http://localhost:8080", webApi) ) {
	Console.WriteLine("Listening...");
	Console.ReadLine();
}

```
* Install the dependant packages ```scriptcs -install -pre```.
* Running as admin type ```scriptcs start.csx``` to launch the app.
* Open a browser to "http://localhost:8080/api/test";
* That's it, your ASP.Net controller is runnning with a OWIN self host server


## What's next
TBD