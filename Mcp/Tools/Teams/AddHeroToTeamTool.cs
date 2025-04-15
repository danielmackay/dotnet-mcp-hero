using ModelContextProtocol.Server;
using System.ComponentModel;
using ApiSdk;
using System;
using System.Threading.Tasks;

namespace HeroMcp.Tools;

[McpServerToolType]
public static class AddHeroToTeamTool
{
    [McpServerTool, Description("Add a hero to a team.")]
    public static async Task<string> AddHeroToTeam(
        [Description("ID of the team")]string teamId,
        [Description("ID of the hero to add")]string heroId,
        HeroApi api)
    {
        if (string.IsNullOrEmpty(teamId))
            return "Error: Team ID is required";

        if (string.IsNullOrEmpty(heroId))
            return "Error: Hero ID is required";

        try
        {
            if (!Guid.TryParse(teamId, out Guid teamGuid))
                return "Error: Invalid Team ID format";

            if (!Guid.TryParse(heroId, out Guid heroGuid))
                return "Error: Invalid Hero ID format";

            await api.Api.Teams[teamGuid].Heroes[heroGuid].PostAsync();

            return $"Hero with ID {heroId} successfully added to team with ID {teamId}";
        }
        catch (Exception ex)
        {
            return $"Error adding hero to team: {ex.Message}";
        }
    }
}