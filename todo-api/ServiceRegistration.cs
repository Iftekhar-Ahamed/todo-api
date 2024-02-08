using todo_api.IRepository;
using todo_api.Repository;
using todo_api.Services;

namespace todo_api
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IAuthentication, AuthenticationRepository>();
            services.AddTransient<DbOperation>();
            services.AddTransient<ITodo, TodoRepository>();
        }
    }
}
