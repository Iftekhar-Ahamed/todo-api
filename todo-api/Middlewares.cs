using todo_api.Middleware;

namespace todo_api
{
    public static class Middlewares
    {
        public static IApplicationBuilder UseCustomAuthorization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomAuthorization>();
        }
    }
}
