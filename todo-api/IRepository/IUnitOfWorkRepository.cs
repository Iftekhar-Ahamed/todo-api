namespace todo_api.IRepository
{
    public interface IUnitOfWorkRepository
    {
        public IAuthentication Authentication { get;}
        public ITodo Todo { get;}

    }
}
