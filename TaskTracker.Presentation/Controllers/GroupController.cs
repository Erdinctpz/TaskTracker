using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskTracker.Application.Abstract;
using TaskTracker.Application.Services;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Presentation.Controllers
{
    [Authorize]
    public class GroupController(
        UserManager<AppUser> userManager,
        IGroupService groupService
    ) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetMyGroups()
        {
            var userId = userManager.GetUserId(User);

            if (userId == null)
            {
                return Unauthorized();
            }

            var result = await groupService.GetAllAsync(userId);

            if (result.IsSuccess)
            {
                return Json(new
                {
                    success = true,
                    data = result.Data
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