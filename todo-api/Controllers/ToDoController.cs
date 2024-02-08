using Microsoft.AspNetCore.Mvc;
using todo_api.IRepository;
using todo_api.Model.TodoModel;

namespace todo_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController: ControllerBase
    {
        private ITodo _Itodo;
        public ToDoController(ITodo todo)
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
    }
}
