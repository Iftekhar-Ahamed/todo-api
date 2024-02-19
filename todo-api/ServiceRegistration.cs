using todo_api.IRepository;
using todo_api.IService;
using todo_api.Repository;
using todo_api.Services;

namespace todo_api
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IAuthentication, AuthenticationRepository>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<ITodo, TodoRepository>();
            services.AddScoped<ITodoService, ToDoService>();
        }
    }
}
