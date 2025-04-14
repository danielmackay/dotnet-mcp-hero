using HeroApi.Domain.Heroes;
using HeroApi.Domain.Teams;
using Vogen;

namespace HeroApi.Infrastructure.Persistence.Configuration;

[EfCoreConverter<TeamId>]
[EfCoreConverter<HeroId>]
[EfCoreConverter<MissionId>]
internal sealed partial class VogenEfCoreConverters;