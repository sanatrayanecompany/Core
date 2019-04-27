using System;
using System.IO;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Core.Base
{
    public static class Host
    {
        internal static Config Config { get; private set; }

        internal static string Path { get; set; }

        private static JavaScriptSerializer serializer;

        public static void Run()
        {
            Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");

            serializer = new JavaScriptSerializer();

            Config = serializer.Deserialize<Config>(ReadFile());

            GlobalConfiguration.Configuration.Filters.Add(new CoreExceptionHandler());

            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new CoreControllerActivator());

            SetCamelCaseJsonResponse();

            //Resolver.RegisterType<IContext, Context>();
            //Resolver.RegisterType<IToken, Token>();
        }

        private static void SetCamelCaseJsonResponse()
        {
            if (Config.CamelCaseJsonResponse)
            {
                JsonSerializerSettings settings = GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings;
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }
        }

        private static string ReadFile()
        {
            string path = System.IO.Path.Combine(Path, "config.json");
            return File.ReadAllText(path);
        }
    }
}