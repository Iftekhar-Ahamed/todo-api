namespace todo_api.Model
{
    public class PaginationLandingModel
    {
        public dynamic data { get; set; }
        public long PageNo { get; set; }
        public long TotalCount { get; set; }
        public long PageSize { get; set; }
        public PaginationLandingModel(dynamic Data,long PageNo,long TotalCount,long PageSize) { 
            this.PageNo = PageNo;
            this.TotalCount = TotalCount;
            this.PageSize = PageSize;
            this.data = Data;
        }
    }
}
