using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.Abstract;
using TaskTracker.Application.DTOs;
using TaskTracker.Domain;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Presentation.Controllers
{
    [Authorize]
    public class TaskController(
        UserManager<AppUser> userManager,
        ITodoService todoService
    ) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetMyTasks()
        {
            var userId = userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            var result = await todoService.GetAllAsync(x => x.UserId == userId && x.TaskGroupId == null);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return PartialView("~/Views/Home/Partials/_TaskList.cshtml", result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskDto createTaskDto)
        {
            var userId = userManager.GetUserId(User);
            var result = await todoService.InsertAsync(createTaskDto, userId);

            if (result.IsSuccess && result.Data != null)
            {
                return Json(new
                {
                    success = true,
                    message = "Görev başarıyla oluşturuldu."
                });
            }

            return Json(new
            {
                success = false,
                message = result.Message
            });
        }
    }
}