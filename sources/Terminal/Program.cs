using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Vardirsoft.Commandorix.Terminal;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<CoreService>();
    })
    .Build();

await host.RunAsync();