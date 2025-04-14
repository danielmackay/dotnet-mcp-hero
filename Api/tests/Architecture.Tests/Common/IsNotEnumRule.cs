using Mono.Cecil;

namespace HeroApi.Architecture.UnitTests.Common;

public class IsNotEnumRule : ICustomRule
{
    public bool MeetsRule(TypeDefinition type) => !type.IsEnum;
}