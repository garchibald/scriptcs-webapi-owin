using System.Linq;
using ScriptCs.Contracts;

namespace ScriptCs.WebApi.Owin
{
    public class ScriptPack : IScriptPack
    {
        IScriptPackContext IScriptPack.GetContext()
        {
            return new WebApiOwin();
        }

        void IScriptPack.Initialize(IScriptPackSession session)
        {
            var namespaces = new[]
                {
                    "System.Web.Http",
                    "System.Net.Http",
                    "System.Net.Http",
                    "System.Web.Http.OData",
                    "System.Web.Http.OData.Builder",
                    "Microsoft.Data.Edm",
                    "Microsoft.Owin.Hosting",
                    "Owin",
                    "System.Web.Http.Dispatcher"
                }.ToList();

            namespaces.ForEach(session.ImportNamespace);

            session.AddReference(@"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\System.Net.Http.dll");
        }

        void IScriptPack.Terminate()
        {
        }
    }
}
