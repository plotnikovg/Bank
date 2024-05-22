using System.Text;
using Bank.WebClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Bank.WebClient.Controllers;

public class AccountController : Controller
{
    private readonly HttpClient _httpClient;
    private Uri _baseAddress = new Uri("http://localhost:5020");
     public AccountController()
     {
         _httpClient = new HttpClient();
         _httpClient.BaseAddress = _baseAddress;
     }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        var req = new HttpRequestMessage(HttpMethod.Post, _httpClient.BaseAddress + "Account/Logout");
        var res = await _httpClient.SendAsync(req);
        
        req = new HttpRequestMessage(HttpMethod.Get, _httpClient.BaseAddress + "Account/Account/CheckLogin");
        res = await _httpClient.GetAsync(_httpClient.BaseAddress + "Account/CheckLogin");
        bool isL = await res.Content.ReadFromJsonAsync<bool>();
        
        var request = new HttpRequestMessage(HttpMethod.Post, _httpClient.BaseAddress + "Account/Login");
        request.Content = new StringContent(JsonConvert.SerializeObject(loginViewModel), Encoding.UTF8, "application/json");
        var response = await _httpClient.SendAsync(request);

        var cookies = ExtractCookiesFromResponse(response);
        
        foreach (var item in cookies)
        {
            Response.Cookies.Append(item.Key, item.Value, new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(5),
                HttpOnly = true,
                IsEssential = true
            });
        }

        if (response.IsSuccessStatusCode)
        {
            // var cookieOptions = new CookieOptions
            // {
            //     Expires = new DateTimeOffset?(DateTimeOffset.Now.AddMinutes(5)),
            //     HttpOnly = true
            // };
            // Response.Cookies.Append("UserLoggedIn", "true", cookieOptions);
            //return RedirectToAction("Index", "Home");
        }
        response = await _httpClient.GetAsync(_httpClient.BaseAddress + "WeatherForecast/GetWeatherForecast");
        //
        request = new HttpRequestMessage(HttpMethod.Get, _httpClient.BaseAddress + "Account/Account/CheckLogin");
        response = await _httpClient.GetAsync(_httpClient.BaseAddress + "Account/CheckLogin");
        bool isLoggedIn = await response.Content.ReadFromJsonAsync<bool>();
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
}