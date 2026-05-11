using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Presentation.Models;

namespace TaskTracker.Presentation.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}