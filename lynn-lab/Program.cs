using lynn_lab.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((context, services) =>
    {
        services.AddHttpClient();
        //services.AddSingleton<LineHelper>();
        services.AddSingleton<HttpTriggerExample>();
        //services.Configure<LineNotifyOption>(context.Configuration.GetSection("LineNotify"));
    })
    .Build();


host.Run();