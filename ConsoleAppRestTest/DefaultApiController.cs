using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;

namespace ConsoleAppRestTest
{
    internal class DefaultApiController : WebApiController
    {
        [Route(HttpVerbs.Get, "/testerState")]
        public TesterState GetTesterState()
        {
            return new TesterState();
        }

        [Route(HttpVerbs.Put, "/measurement/start/{workPieceId}")]
        public void PutMeasurementStart(string workPieceId)
        {
            if (workPieceId == "0815")
            {
                Response.StatusCode = 405;
                Response.StatusDescription += ": Don't do this.";
            }
        }
    }
}