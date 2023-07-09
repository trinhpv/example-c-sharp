using System.ComponentModel.DataAnnotations;
using static TasksMVC.Controllers.WorkController;

namespace TasksMVC.Models
{
    public class Work
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime HandleDate { get; set; }
        public TaskStatus Status { get; set; }
    }

    public enum TaskStatus
    {
        Need = 0,
        Inprogress = 1,
        Done = 2
    }
    public enum OrderBy
    {
        Id,
        Name,
        CreatedDate,
        HandleDate,
    }
    public enum OrderType
    {
        Ascending = 1,
        Descending = 2

    }
    public class WorkRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime? CreatedDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? HandleDate { get; set; }
        public TaskStatus? Status { get; set; }
        public OrderBy orderBy { get; set; } = OrderBy.Id;
        public TaskStatus? status { get; set; }
        public DateTime? searchDate { get; set; }
        public OrderType orderType { get; set; } = OrderType.Ascending;
        public string? searchString { get; set; }
        public int page { get; set; } = 1;
        public int perPage { get; set; } = 10;
    }
}
