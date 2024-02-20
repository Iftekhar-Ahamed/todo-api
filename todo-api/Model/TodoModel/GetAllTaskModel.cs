namespace todo_api.Model.TodoModel
{
    public class GetAllTaskModel
    {
        public int TaskId { get; set; }
        public long PriorityId { get; set; }
        public string PriorityName { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public long UserId { get; set; }
        public DateTime ExpireDateTime { get; set; }
        public string CreateBy { get; set; }
        public long CreatorId { get; set; }
        public string Assigned { get; set; }
        public long AssignedId { get; set; }

        public Boolean Status { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
