using System.Net;
using lynn_lab.Helper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace lynn_lab.Services;

public class HttpTriggerExample
{
    private readonly LineHelper _lineHelper;
    private readonly IHttpClientFactory _httpClientFactory;

    public HttpTriggerExample(LineHelper lineHelper,IHttpClientFactory httpClientFactory)
    {
        _lineHelper = lineHelper;
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
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        response.WriteString("Welcome to Azure Functions!");

        return response;
    }
}