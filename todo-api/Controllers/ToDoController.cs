using Microsoft.AspNetCore.Mvc;
using todo_api.Helper;
using todo_api.IRepository;
using todo_api.IService;
using todo_api.Model.TodoModel;

namespace todo_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController: ControllerBase
    {
        private ITodoService _Itodo;
        public ToDoController(ITodoService todo)
        {
            _Itodo = todo;
        }
        [HttpPost]
        [Route("CreateTask")]
        public async Task<IActionResult> CreateTask(CreateTaskModel createTaskModel)
        {
            var res = await _Itodo.CreateTaskAsync(createTaskModel);
            return Ok(res);
        }
        [HttpGet]
        [Route("GetAllTaskByUserId")]
        public async Task<IActionResult> GetAllTaskByUserId(long UserId, string OrderBy, long PageNo, long PageSize)
        {
            var res = await _Itodo.GetAllTaskByUserIdAsync(UserId,OrderBy, PageNo,PageSize);
            return Ok(res);
        }
        [HttpGet]
        [Route("GetPriorityDDL")]
        public async Task<IActionResult> GetPriorityDDL(string OrderBy)
        {

            var res = await _Itodo.GetPriorityDDLAsync(OrderBy);
            return Ok(res);
        }
        [HttpPost]
        [Route("UpdateTaskByTaskId")]
        public async Task<IActionResult> UpdateTaskByTaskId(UpdateTaskModel createTaskModel)
        {
            var res = await _Itodo.UpdateTaskByTaskIdAsync(createTaskModel);
            return Ok(res);
        }
        [HttpPost]
        [Route("DeleteTaskByTaskId")]
        public async Task<IActionResult> DeleteTaskByTaskId( long TaskId)
        {
            var res = await _Itodo.DeleteTaskByTaskIdAsync(TaskId);
            return Ok(res);
        }
    }
}
