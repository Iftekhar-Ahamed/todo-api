using todo_api.IRepository;
using todo_api.IService;

namespace todo_api.Services
{
    public class UnitOfWorkService:IUnitOfWorkService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        public UnitOfWorkService(IUnitOfWorkRepository unitOfWorkRepository, IConfiguration configuration)
        {
            _configuration = configuration;
            _unitOfWorkRepository = unitOfWorkRepository;
        }

        IAuthenticationService IUnitOfWorkService.Authentication =>  new AuthenticationService(_unitOfWorkRepository,_configuration);


        ITodoService IUnitOfWorkService.TodoService => new ToDoService(_unitOfWorkRepository);
    }
}
