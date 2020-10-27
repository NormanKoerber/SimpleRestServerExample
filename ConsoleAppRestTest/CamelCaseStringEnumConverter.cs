using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace ConsoleAppRestTest
{

    internal class CamelCaseStringEnumConverter : StringEnumConverter
    {
        public CamelCaseStringEnumConverter() : base(new CamelCaseNamingStrategy())
        {
        }
    }
}