using Scalar.AspNetCore;

namespace HeroApi.WebApi.Extensions;

public static class CustomScalarExt
{
    public static void MapCustomScalarApiReference(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapScalarApiReference(options => options.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient));
    }
}