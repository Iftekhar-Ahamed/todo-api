﻿using todo_api.Helper;
using todo_api.IRepository;
using todo_api.Model.TodoModel;
using todo_api.Model;
using todo_api.IService;

namespace todo_api.Services
{
    public class ToDoService: ITodoService
    {
        public readonly ITodo db;
        public ToDoService(ITodo todo) {
            db = todo;
        }
        public async Task<MessageHelperModel> CreateTaskAsync(CreateTaskModel CreateTaskModel)
        {
            var res =await db.CreateTaskAsync(CreateTaskModel);
            var msg = new MessageHelperModel { StatusCode = 200, Message = "" };
            if (res == 0)
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
            var res = await db.GetAllTaskByUserIdAsync(UserId, OrderBy, PageNo, PageSize);
            PaginationLandingModel pagination = new PaginationLandingModel(res, PageNo, res.Count, PageSize);
            return pagination;
        }
        public async Task<List<CommonDDL>> GetPriorityDDLAsync(string OrderBy)
        {
            var res = await db.GetPriorityDDLAsync(OrderBy);
            return res;
        }
        public async Task<MessageHelperModel> UpdateTaskByTaskIdAsync(UpdateTaskModel UpdateTaskModel)
        {
            var res = await db.UpdateTaskByTaskIdAsync(UpdateTaskModel);
            MessageHelperModel msg = new MessageHelperModel();
            if (res > 0)
            {
                msg.Message = "Sucessfully Updated";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "Faild Updated";
                msg.StatusCode = 400;
            }
            return msg;
        }
        public async Task<MessageHelperModel> DeleteTaskByTaskIdAsync(long TaskId)
        {
            var res = await db.DeleteTaskByTaskIdAsync(TaskId);
            MessageHelperModel msg = new MessageHelperModel();
            if (res > 0)
            {
                msg.Message = "Sucessfully Deleted";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "Faild Delete";
                msg.StatusCode = 400;
            }
            return msg;
        }
    }
}