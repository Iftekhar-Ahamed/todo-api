namespace todo_api.IService
{
    public interface IUnitOfWorkService
    {
        public IAuthenticationService Authentication { get;}
        public ITodoService TodoService { get;}
    }
}
