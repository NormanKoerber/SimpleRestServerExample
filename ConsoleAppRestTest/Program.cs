using EmbedIO;
using EmbedIO.Utilities;
using EmbedIO.WebApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppRestTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:9696/";
            if (args.Length > 0)
                url = args[0];

            // Our web server is disposable.
            using (var server = CreateWebServer(url))
            {
                // Once we've registered our modules and configured them, we call the RunAsync() method.
                server.RunAsync();

                var browser = new System.Diagnostics.Process()
                {
                    StartInfo = new System.Diagnostics.ProcessStartInfo(url) { UseShellExecute = true }
                };
                browser.Start();
                // Wait for any key to be pressed before disposing of our web server.
                // In a service, we'd manage the lifecycle of our web server using
                // something like a BackgroundWorker or a ManualResetEvent.
                Console.ReadKey(true);
            }
        }

        // Create and configure our web server.
        private static WebServer CreateWebServer(string url)
        {
            var server = new WebServer(o => o
                    .WithUrlPrefix(url)
                    .WithMode(HttpListenerMode.EmbedIO))
                // First, we will configure our web server by adding Modules.
                .WithLocalSessionManager()
                .WithWebApi("/api", SerializationCallback, m => m
                    .WithController<DefaultApiController>());

            // Listen for state changes.
            server.StateChanged += (s, e) => Console.WriteLine($"WebServer New State - {e.NewState}");

            return server;
        }

        public static async Task SerializationCallback(IHttpContext context, object data)
        {
            Validate.NotNull(nameof(context), context).Response.ContentType = MimeType.Json;

            using (var text = context.OpenResponseText(new UTF8Encoding(false)))
            {
                await text.WriteAsync(
                    JsonConvert.SerializeObject(
                        data,
                        new JsonSerializerSettings()
                        {
                            ContractResolver = GetDefaultResolver()
                        }))
                    .ConfigureAwait(false);
            }

            DefaultContractResolver GetDefaultResolver() => new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy { OverrideSpecifiedNames = false }
            };
        }
    }
}
