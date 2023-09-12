using System.ComponentModel.DataAnnotations;

namespace TaskTimelinePlanner.Models
{
    public class TaskRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Interval { get; set; }

        [Required]
        public DayOfWeek DayOfWeek { get; set; }
    }
}
