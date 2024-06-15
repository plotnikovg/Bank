using System.Text;
using Bank.WebClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Bank.WebClient.Controllers;

[Route("Transfer")]
public class TransferController : Controller
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly Uri _baseAddress = new Uri("http://localhost:5020");
    private readonly ILogger<TransferController> _logger;

    public TransferController(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor, ILogger<TransferController> logger)
    {
        _clientFactory = clientFactory;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    [HttpGet]
    [Route("Transfer")]
    public IActionResult Transfer()
    {
        return View();
    }
    [HttpPost]
    [Route("Transfer1")]
    public async Task<IActionResult> Transfer(TransferPostViewModel transferPostViewModel)
    {
        using HttpClient httpClient = _clientFactory.CreateClient();
        httpClient.BaseAddress = _baseAddress;
        
        var clientIp = Request.HttpContext.Connection.RemoteIpAddress?.ToString();
        
        var clientUserAgent = Request.HttpContext.Request.Headers.UserAgent.ToString();
        var uaParser = Parser.GetDefault();
        var clientBrowserResult = uaParser.Parse(clientUserAgent);
        var clientBrowser = clientBrowserResult.UA.Family + "/" + clientBrowserResult.UA.Major + "." + clientBrowserResult.UA.Minor;
        
        if (clientIp == null || clientBrowser == null)
            return View();

        transferPostViewModel.Ip = clientIp;
        transferPostViewModel.Browser = clientBrowser;

        var authorizationToken = Request.Cookies["Token"];
        var request = new HttpRequestMessage(HttpMethod.Post, _baseAddress + "Client/Transfer");
        request.Headers.Add("Authorization", "Bearer " + authorizationToken);
        request.Content = new StringContent(JsonConvert.SerializeObject(
            transferPostViewModel), Encoding.UTF8, "application/json");
        var response = await httpClient.SendAsync(request);
        return View();
    }
}