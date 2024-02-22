namespace todo_api.Model.TodoModel
{
    public class TaskListWithCountModel
    {
        public List<GetAllTaskModel> TaskList { get; set; }
        public  long TaskCount { get; set; }
    }
}
