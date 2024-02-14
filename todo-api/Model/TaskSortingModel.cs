using System.ComponentModel.DataAnnotations;

namespace todo_api.Model
{
    public class TaskSortingModel
    {
        [StringLength(5)]
        public string? Priority { get; set; }
        [StringLength(5)]
        public string? creationDate { get; set; }
        public string? Status { get; set; }
    }
}
