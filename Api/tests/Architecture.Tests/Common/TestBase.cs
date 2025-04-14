using HeroApi.Application.Common.Interfaces;
using HeroApi.Domain.Common.Base;
using HeroApi.Infrastructure.Persistence;
using HeroApi.WebApi;
using System.Reflection;

namespace HeroApi.Architecture.UnitTests.Common;

public abstract class TestBase
{
    protected static readonly Assembly DomainAssembly = typeof(AggregateRoot<>).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(IApplicationDbContext).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(ApplicationDbContext).Assembly;
    protected static readonly Assembly PresentationAssembly = typeof(IWebApiMarker).Assembly;
}