using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Sentry;

namespace Company.Function
{
    public class FunctionEntryPoint
    {
        private readonly ILogger _logger;

        public FunctionEntryPoint(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<FunctionEntryPoint>();
        }

        [Function("FunctionEntryPoint")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            using (SentrySdk.Init(o =>
    {
        o.Dsn = "https://63a8198a353749d384cf367abd3c7b38@o1281105.ingest.sentry.io/6486335";
        // When configuring for the first time, to see what the SDK is doing:
        o.Debug = true;
        // Set traces_sample_rate to 1.0 to capture 100% of transactions for performance monitoring.
        // We recommend adjusting this value in production.
        o.TracesSampleRate = 1.0;
    }))
            {
                // App code goes here. Dispose the SDK before exiting to flush events.
                SentrySdk.CaptureMessage("Something went wrong from your Isolate .NET 6 function");

            }


            return response;
        }
    }
}
