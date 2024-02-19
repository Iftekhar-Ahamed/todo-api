using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using todo_api.IService;
using todo_api.Model;
using todo_api.Model.TodoModel;

namespace todo_api.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> GetAllTaskByUserId(long UserId, string? SearchTerm, [FromQuery] TaskSortingModel taskSorting, long PageNo, long PageSize)
        {
            var res = await _Itodo.GetAllTaskByUserIdAsync(UserId, SearchTerm, taskSorting, PageNo, PageSize);
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
        public async Task<IActionResult> DeleteTaskByTaskId( [FromBody]long TaskId)
        {
            var res = await _Itodo.DeleteTaskByTaskIdAsync(TaskId);
            return Ok(res);
        }
        [HttpGet]
        [Route("UserTaskReport")]
        public async Task<IActionResult> UserTaskReport(long UserId)
        {
            var res = await _Itodo.UserTaskReport(UserId);
            return Ok(res);
        }
    }
}
