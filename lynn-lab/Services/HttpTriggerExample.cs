using System.Net;
using lynn_lab.Helper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace lynn_lab.Services;

public class HttpTriggerExample
{
    private readonly IHttpClientFactory _httpClientFactory;

    public HttpTriggerExample(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [Function("HttpTriggerExample")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "api/HttpTriggerExample")]
        HttpRequestData req,
        FunctionContext executionContext)
    {
        var logger = executionContext.GetLogger("HttpTriggerExample");
        logger.LogInformation("C# HTTP trigger function processed a request.");
        var client = _httpClientFactory.CreateClient();
        await client.PostAsync("https://webhook.site/895043d5-450e-47af-8825-de5b999d79c7", new StringContent("Hello World"));
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        response.WriteString("Welcome to Azure Functions!");

        return response;
    }
}