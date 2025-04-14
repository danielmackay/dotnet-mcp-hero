using Microsoft.EntityFrameworkCore;

namespace HeroApi.WebApi.HealthChecks.EntityFrameworkDbContextHealthCheck;

public sealed class EntityFrameworkDbContextHealthCheckOptions<TContext> where TContext : DbContext
{
    public Func<TContext, CancellationToken, Task<DbHealthCheckResult>>? TestQuery { get; set; }
}