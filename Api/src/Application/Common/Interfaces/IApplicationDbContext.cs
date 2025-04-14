using HeroApi.Domain.Heroes;
using HeroApi.Domain.Teams;

namespace HeroApi.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Hero> Heroes { get; }
    DbSet<Team> Teams { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}