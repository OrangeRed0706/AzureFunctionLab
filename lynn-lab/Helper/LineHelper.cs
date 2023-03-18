using lynn_lab.Options;
using Microsoft.Extensions.Options;

namespace lynn_lab.Helper;

public sealed class LineHelper
{
    private readonly LineNotifyOption _options;

    public LineHelper(IOptions<LineNotifyOption> options)
    {
        _options = options.Value;
    }

    /// <summary>
    /// 發送 Line Notify 通知
    /// </summary>
    /// <param name="message">The message.</param>
    /// <exception cref="System.Exception">發送失敗</exception>
    public async Task SendLineNotify(string message)
    {
        if (string.IsNullOrEmpty(_options.Token))
        {
            throw new Exception("取得 Line Notify Token 失敗");
        }

        const string lineNotifyApi = "https://notify-api.line.me/api/notify";
        var requestBody = new Dictionary<string, string>
        {
            ["message"] = message
        };

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(lineNotifyApi),
            Content = new FormUrlEncodedContent(requestBody)
        };
        request.Headers.Add("Authorization", $"Bearer {_options.Token}");

        using var client = new HttpClient();
        var response = await client.SendAsync(request);

        if (response.IsSuccessStatusCode is false)
        {
            throw new Exception("Line Notify 發送失敗");
        }
    }
}