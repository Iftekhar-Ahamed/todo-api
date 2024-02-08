﻿namespace todo_api.Model.TodoModel
{
    public class CreateTaskModel
    {
        public long PriorityId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public long UserId { get; set; }
        public Boolean Status { get; set; }
        public DateTime ExpireDateTime { get; set; }
        public DateTime CreationDateTime { get; set;}
    }
}
