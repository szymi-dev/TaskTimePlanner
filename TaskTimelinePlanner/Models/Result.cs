using System.ComponentModel.DataAnnotations;

namespace TaskTimelinePlanner.Models
{
    public class Result
    {
        [Key]
        public int Id { get; set; }
        public DateTime Today { get; set; }
        public DateTime FirstOccurrence { get; set; }
        public int OccurrenceCount { get; set; }
        public DateTime LastOccurrence { get; set; }
        public DateTime NextExecution { get; set; }
    }
}
