using TaskTracker.Application.DTOs;

namespace TaskTracker.Presentation.Models
{
    public class HomeDashBoardViewModel
    {
        public List<TaskDto> Tasks { get; set; } = [];
        //public List<GroupDto> Groups { get; set; }
    }
}