using System.ComponentModel.DataAnnotations;

namespace todo_api.Model.TodoModel
{
    public class CreateTaskModel
    {
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Please enter Priority value bigger than {1}")]
        public long PriorityId { get; set; }
        [Required]
        [StringLength(50)]
        public string TaskName { get; set; }
        [StringLength(250)]
        public string TaskDescription { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required]
        public Boolean Status { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ExpireDateTime { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreationDateTime { get; set;}
        [Required]
        public long CreatorId { get; set; }
        [Required]
        public long AssignedId { get; set; }
    }
}
