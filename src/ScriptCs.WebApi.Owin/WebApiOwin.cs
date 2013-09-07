using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Owin;
using ScriptCs.Contracts;
using ScriptCs.Owin;

namespace ScriptCs.WebApi.Owin
{
    public class WebApiOwin : IScriptPackContext, IOwinStartup
    {
        public HttpConfiguration Configuration { get; set; }

        public WebApiOwin()
        {
            // Create default server
            CreateServer();
        }

        public void CreateServer(HttpConfiguration config, ICollection<Type> controllerTypes)
        {
            Contract.Requires(controllerTypes != null);
            Contract.Requires(ControllerResolver.AllAssignableToIHttpController(controllerTypes));

            config.Services.Replace(typeof(IHttpControllerTypeResolver), new ControllerResolver(controllerTypes));

            config.Routes.MapHttpRoute(name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            Configuration = config;
        }

        public void CreateServer(HttpConfiguration config, params Assembly[] assemblies)
        {
            var controllerAssemblies = new List<Assembly>(AppDomain.CurrentDomain.GetAssemblies()).Union(assemblies);
            var types = controllerAssemblies.SelectMany(a => a.GetTypes()).ToArray();
            var controllerTypes = ControllerResolver.WhereControllerType(types).ToList();
            CreateServer(config, controllerTypes);
        }

        public void CreateServer()
        {
            var types = GetLoadedTypes();
            var controllerTypes = ControllerResolver.WhereControllerType(types).ToList();
            CreateServer(new HttpConfiguration(), controllerTypes);
        }

        public IEnumerable<Type> GetLoadedTypes()
        {
            var types = new List<Type>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                types.AddRange(assembly.GetTypes());    
            }
            return types;
        }

        public void Use(IAppBuilder app)
        {
            Contract.Requires(Configuration != null);

            app.UseWebApi(Configuration);
        }
    }
}