using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.Abstract;
using TaskTracker.Domain.Entities;
using TaskTracker.Presentation.Models;

namespace TaskTracker.Presentation.Controllers;

[Authorize]
public class HomeController(
    UserManager<AppUser> userManager,
    ITodoService todoService
) : Controller
{
    public async Task<IActionResult> Index()
    {
        var userId = userManager.GetUserId(User);
        var taskResult = await todoService.GetAllAsync(x => x.UserId == userId && x.TaskGroupId == null);

        var viewModel = new HomeDashBoardViewModel
        {
            Tasks = taskResult.Data ?? []
        };

        return View(viewModel);
    }

    [AllowAnonymous]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}