namespace todo_api.Model.ReportModel
{
    public class TaskReportModel
    {
        public long TotalTask { get; set; }
        public long CompletedTask { get; set; }
        public List<PriorityWiseTaskModel> TaskPriorityWiseReport { get; set; }
    }
}
