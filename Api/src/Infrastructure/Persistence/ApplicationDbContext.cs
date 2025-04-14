using Microsoft.EntityFrameworkCore;
using HeroApi.Application.Common.Interfaces;
using HeroApi.Domain.Common.Interfaces;
using HeroApi.Domain.Heroes;
using HeroApi.Domain.Teams;
using HeroApi.Infrastructure.Persistence.Configuration;
using System.Reflection;

namespace HeroApi.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<Hero> Heroes => AggregateRootSet<Hero>();

    public DbSet<Team> Teams => AggregateRootSet<Team>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.RegisterAllInVogenEfCoreConverters();
    }

    private DbSet<T> AggregateRootSet<T>() where T : class, IAggregateRoot => Set<T>();
}