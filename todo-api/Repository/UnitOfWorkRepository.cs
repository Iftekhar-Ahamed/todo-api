using todo_api.IRepository;

namespace todo_api.Repository
{
    public class UnitOfWorkRepository:IUnitOfWorkRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthentication _authentication;
        private readonly ITodo _todo;
        public UnitOfWorkRepository(IConfiguration configuration) { 
            _configuration = configuration;
        }

        IAuthentication IUnitOfWorkRepository.Authentication => _authentication ?? new AuthenticationRepository(_configuration);

        ITodo IUnitOfWorkRepository.Todo => _todo?? new TodoRepository(_configuration);
    }
}
