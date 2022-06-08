using Microsoft.Extensions.Hosting;
using Sentry;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .Build();

    

host.Run();