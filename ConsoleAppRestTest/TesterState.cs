using Newtonsoft.Json;

namespace ConsoleAppRestTest
{
    internal enum OperatingMode
    {
        Manual = 0,
        Automatic = 1
    }

    internal class TesterState
    {
        public bool IsReady { get; set; }

        public bool NormalizationIsRequired { get; set; }

        public bool IsInBaseposition { get; set; }

        // ToDo: Add further properties

        [JsonConverter(typeof(CamelCaseStringEnumConverter))]
        public OperatingMode OperatingMode { get; set; }
    }
}