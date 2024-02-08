using todo_api.Helper;
using todo_api.IRepository;
using todo_api.Model;
using todo_api.Model.TodoModel;
using todo_api.Services;
namespace todo_api.Repository
{
    public class TodoRepository : ITodo
    {
        private DbOperation db;
        public TodoRepository(DbOperation dbOperation) { 
            this.db = dbOperation;
        }
        public async Task<MessageHelperModel> CreateTaskAsync(CreateTaskModel createTaskModel)
        {
            var res = await db.CreateTaskAsync(createTaskModel);
            var msg = new MessageHelperModel { StatusCode = 200, Message = "" };
            if (res==0)
            {
                msg.Message = "Faild To Create";
            }
            else
            {
                msg.Message = "Sucessfully Added";
            }
            return msg;
        }
        public async Task<PaginationLandingModel> GetAllTaskByUserIdAsync(long UserId, string OrderBy, long PageNo, long PageSize)
        {
            var res = await db.GetAllTaskByUserIdAsync(UserId,OrderBy, PageNo*PageSize,PageSize);
            PaginationLandingModel pagination = new PaginationLandingModel(res,PageNo,res.Count,PageSize);
            return pagination;
        }
    }
}
