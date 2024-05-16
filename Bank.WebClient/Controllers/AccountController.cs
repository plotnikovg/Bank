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
        

        var request = new HttpRequestMessage(HttpMethod.Post, _httpClient.BaseAddress + "Account/Login");
        request.Content = new StringContent(JsonConvert.SerializeObject(loginViewModel), Encoding.UTF8, "application/json");
        var response = await _httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }
}