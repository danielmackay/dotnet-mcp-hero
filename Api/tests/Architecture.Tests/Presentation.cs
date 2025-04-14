using HeroApi.Application.Common.Interfaces;
using HeroApi.Architecture.UnitTests.Common;
using HeroApi.Infrastructure.Persistence;

namespace HeroApi.Architecture.UnitTests;

public class Presentation : TestBase
{
    private static readonly Type IDbContext = typeof(IApplicationDbContext);
    private static readonly Type DbContext = typeof(ApplicationDbContext);

    [Fact]
    public void Endpoints_ShouldNotReferenceDbContext()
    {
        var types = Types
            .InAssembly(PresentationAssembly)
            .That()
            .HaveNameEndingWith("Endpoints");

        var result = types
            .ShouldNot()
            .HaveDependencyOnAny(DbContext.FullName, IDbContext.FullName)
            .GetResult();

        result.Should().BeSuccessful();
    }
}