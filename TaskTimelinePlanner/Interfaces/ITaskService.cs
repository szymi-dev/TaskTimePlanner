using Microsoft.AspNetCore.Mvc;
using TaskTimelinePlanner.Models;

namespace TaskTimelinePlanner.Interfaces
{
    public interface ITaskService
    {
        Task<ActionResult<Result>> CalculateNextExecution(TaskRequest taskDate);
        Task<ActionResult<Result>> GetLastCalculationResult();
    }
}
