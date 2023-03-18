using lynn_lab.Helper;
using lynn_lab.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((context, services) =>
    {
        services.AddHttpClient();
        services.AddSingleton<LineHelper>();
        services.Configure<LineNotifyOption>(context.Configuration.GetSection("LineNotify"));
    })
    .Build();


host.Run();