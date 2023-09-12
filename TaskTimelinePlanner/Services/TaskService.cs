using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTimelinePlanner.Data;
using TaskTimelinePlanner.Interfaces;
using TaskTimelinePlanner.Models;

namespace TaskTimelinePlanner.Services
{
    public class TaskService : ITaskService
    {
        private readonly DataContext _context;
        public TaskService(DataContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<Result>> CalculateNextExecution(TaskRequest taskDate)
        {
            var currentDate = DateTime.Today;
            var interval = taskDate.Interval;
            var startDate = taskDate.StartDate;
            var firstExecutionDay = taskDate.DayOfWeek;

            DateTime FirstExecutionDate = startDate.AddDays(1);
            DateTime OccurancesMaxDate = currentDate;
            DateTime LastExecutionDate = currentDate.AddDays(-1);
            DateTime NextExecutionDate = currentDate.AddDays(1);


            while (FirstExecutionDate.DayOfWeek != firstExecutionDay)
            {
                FirstExecutionDate = FirstExecutionDate.AddDays(1);
            }

            while (LastExecutionDate.DayOfWeek != firstExecutionDay)
            {
                LastExecutionDate = LastExecutionDate.AddDays(-1);
            }

            while (NextExecutionDate.DayOfWeek != firstExecutionDay)
            {
                NextExecutionDate = NextExecutionDate.AddDays(1);
            }

            int occurrences = 0;
            DateTime tempDate = FirstExecutionDate;
            while (tempDate <= OccurancesMaxDate)
            {
                occurrences++;
                tempDate = tempDate.AddDays(7 * interval);
            }

            var result = new Result
            {
                Today = currentDate,
                FirstOccurrence = FirstExecutionDate,
                OccurrenceCount = occurrences,
                LastOccurrence = LastExecutionDate,
                NextExecution = NextExecutionDate
            };

            _context.Results.Add(result);
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<ActionResult<Result>> GetLastCalculationResult()
        {
            var lastCalculationResult = await _context.Results
                    .OrderByDescending(t => t.Id)
                    .FirstOrDefaultAsync() ?? throw new ArgumentException("No result found");

            return lastCalculationResult;
        }
    }
}
