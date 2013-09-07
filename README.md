scriptcs-owin
===============

# Owin Self Host Script Pack

## What is it?
Makes using Owin self host with scriptcs

## Highlights:

* Creates a self hosted OWIN http server host.
* Allows instances of IOwinStartup to plug in other OWIN middleware components

## Getting started with OWIN using the pack

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
* Running as admin type ```scriptcs start.csx``` to launch the app.
* Open a browser to "http://localhost:8080/api/test";
* That's it, your OWIN self is running with a webapi!

## Customizing
You can customize the componants loaded by passing in additional IOwinStartup

## What's next
TBD