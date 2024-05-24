using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Bank.WebClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Bank.WebClient.Controllers;

public class AccountController : Controller
{
    private readonly IHttpClientFactory _clientFactory;
    private Uri _baseAddress = new Uri("http://localhost:5020");
     public AccountController(IHttpClientFactory clientFactory)
     {
         _clientFactory = clientFactory;
     }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        using HttpClient httpClient = _clientFactory.CreateClient();
        httpClient.BaseAddress = _baseAddress;
        
        // var req = new HttpRequestMessage(HttpMethod.Post, httpClient.BaseAddress + "Account/Logout");
        // var res = await httpClient.SendAsync(req);
        //
        // req = new HttpRequestMessage(HttpMethod.Get, httpClient.BaseAddress + "Account/Account/CheckLogin");
        // res = await httpClient.GetAsync(httpClient.BaseAddress + "Account/CheckLogin");
        // bool isL = await res.Content.ReadFromJsonAsync<bool>();
        
        var request = new HttpRequestMessage(HttpMethod.Post, httpClient.BaseAddress + "Account/Login");
        request.Content = new StringContent(JsonConvert.SerializeObject(loginViewModel), Encoding.UTF8, "application/json");
        var response = await httpClient.SendAsync(request);


        //var cookies = ExtractCookiesFromResponse(response);
        var cookies = ExtractCookiesFromResponseList(response);
        
        foreach (var item in cookies)
        {
            Response.Cookies.Append(item.Name, item.Value, new CookieOptions
            {
                Expires = item.Expires,
                HttpOnly = item.HttpOnly,
                Secure = item.Secure,
                IsEssential = true
                //IsEssential = true
            });
            // request.Headers.Add("Cookie", $"@{Response.Cookies.ToString()}");
        }
        
        if (response.IsSuccessStatusCode)
        {
             var cookieOptions = new CookieOptions
             {
                 Expires = new DateTimeOffset?(DateTimeOffset.Now.AddMinutes(5)),
                 HttpOnly = true
             };
             //Response.Cookies.Append("UserLoggedIn", "true", cookieOptions);
            // return RedirectToAction("Index", "Home");
        }
        
        Request.Cookies.TryGetValue("Token", out string? token);
        if (token != null)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            response = await httpClient.GetAsync(httpClient.BaseAddress + "WeatherForecast");
        }

        response = await httpClient.GetAsync(httpClient.BaseAddress + "Account/CheckLogin");
        string isLoggedIn = await response.Content.ReadFromJsonAsync<string>();
        return View();
    }
    [NonAction]
    public static IDictionary<string, string> ExtractCookiesFromResponse(HttpResponseMessage response)
    {
        IDictionary<string, string> result = new Dictionary<string, string>();
        IEnumerable<string> values;
        if (response.Headers.TryGetValues("Set-Cookie", out values))
        {
            Microsoft.Net.Http.Headers.SetCookieHeaderValue.ParseList(values.ToList()).ToList().ForEach(cookie =>
            {
                result.Add(cookie.Name.Value, cookie.Value.Value);
            });
        }
        return result;
    }
    public static List<Cookie> ExtractCookiesFromResponseList(HttpResponseMessage response)
    {
        List<Cookie> result = [];
        IEnumerable<string> values;
        if (response.Headers.TryGetValues("Set-Cookie", out values))
        {
            Microsoft.Net.Http.Headers.SetCookieHeaderValue.ParseList(values.ToList()).ToList().ForEach(cookie =>
            {
                result.Add(new Cookie
                {
                    Name = cookie.Name.Value,
                    Value = cookie.Value.Value,
                    Expires = cookie.Expires.Value.UtcDateTime,
                    HttpOnly = cookie.HttpOnly,
                    Secure = cookie.Secure
                });
            });
        }
        return result;
    }
    
}