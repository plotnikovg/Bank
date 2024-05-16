using Microsoft.AspNetCore.Mvc;

namespace Bank.WebClient.Controllers;

public class TransferController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}