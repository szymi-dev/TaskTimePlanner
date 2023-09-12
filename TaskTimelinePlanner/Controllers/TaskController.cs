using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTimelinePlanner.Interfaces;
using TaskTimelinePlanner.Models;

namespace TaskTimelinePlanner.Controllers
{
    public class TaskController : BaseController
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost("CalculateNextExecution")]
        public async Task<ActionResult<Result>> CalculateNextExecution([FromBody] TaskRequest taskDate)
        {
            return await _taskService.CalculateNextExecution(taskDate);
        }

        [HttpGet("GetLastCalculationResult")]
        public async Task<ActionResult<Result>> GetLastCalculationResult()
        {
            return await _taskService.GetLastCalculationResult();
        }
    }
}
