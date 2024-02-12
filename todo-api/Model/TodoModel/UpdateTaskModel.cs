namespace todo_api.Model.TodoModel
{
    public class UpdateTaskModel
    {
        public long TaskId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime ExpireDateTime { get; set; }
        public long PriorityId { get; set; }
        public bool Status { get; set; }
    }
}
