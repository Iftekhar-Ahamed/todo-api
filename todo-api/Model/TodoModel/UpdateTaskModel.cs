using System.ComponentModel.DataAnnotations;

namespace todo_api.Model.TodoModel
{
    public class UpdateTaskModel
    {
        [Required]
        public long TaskId { get; set; }
        [Required]
        [StringLength(50)]
        public string TaskName { get; set; }
        [Required]
        [StringLength(250)]
        public string TaskDescription { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreationDateTime { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ExpireDateTime { get; set; }
        public long PriorityId { get; set; }
        public long AssignedId {  get; set; }
        public bool Status { get; set; }
        [Required]
        public long UserId { get; set; }
    }
}
