using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using todo_api.Helper;
using todo_api.IService;
using todo_api.Model.AuthModel;

namespace todo_api.Middleware
{
    public class CustomAuthorization
    {
        private static readonly object cacheLock = new object();
        private readonly RequestDelegate _nextState ;
        public CustomAuthorization(RequestDelegate nextSate)
        {
            _nextState = nextSate;
        }
        public async Task InvokeAsync(HttpContext httpContext, IUnitOfWorkService _unitOfWorkService, IMemoryCache _memoryCache)
        {
            try
            {

                var endPoint = httpContext.GetEndpoint();

                if (endPoint != null)
                {
                    var routePattern = (endPoint as RouteEndpoint)?.RoutePattern?.RawText;

                    var allowAnonymous = endPoint.Metadata.GetMetadata<AllowAnonymousAttribute>() != null;
                    var authorize = endPoint?.Metadata?.GetMetadata<AuthorizeAttribute>() != null;

                    if (allowAnonymous || (!allowAnonymous && !authorize))
                    {
                        await _nextState(httpContext);
                        return;
                    }

                    var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                    if (token == null)
                    {
                        throw new Exception("Token Not Found");
                    }

                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                    if (jsonToken == null)
                    {
                        throw new Exception("Invalid Token");
                    }



                    DateTime expirationTime = jsonToken.ValidTo;
                    if (expirationTime < DateTime.UtcNow)
                    {
                        var user = new UserInfoModel
                        {
                            FirstName = httpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Name)?.Value ?? "",
                            UserId = long.Parse(httpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value ?? "0"),
                        };

                        lock (cacheLock)
                        {
                            httpContext.Response.StatusCode = 401;

                            if (_memoryCache.TryGetValue(token, out string? cachedToken))
                            {
                                httpContext.Response.WriteAsync("TOKEN " + cachedToken);
                            }
                            else
                            {
                                string newToken = _unitOfWorkService.Authentication.GenerateToken(user);
                                httpContext.Response.WriteAsync("TOKEN " + newToken);

                                var cacheEntryOption = new MemoryCacheEntryOptions
                                {
                                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
                                };
                                _memoryCache.Set(token, newToken, cacheEntryOption);
                            }

                        }
                        
                        return;
                    }
                    else {
                        await _nextState(httpContext);
                        return;
                    }
                }
                else
                {
                    throw new Exception("URL Is Not Valid");
                }

            }catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
