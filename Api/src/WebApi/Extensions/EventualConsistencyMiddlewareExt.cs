using HeroApi.Infrastructure.Middleware;

namespace HeroApi.WebApi.Extensions;

public static class EventualConsistencyMiddlewareExt
{
    public static IApplicationBuilder UseEventualConsistencyMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<EventualConsistencyMiddleware>();
        return app;
    }
}