using ApiSdk;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;

namespace HeroMcp.Tools.Teams;

[McpServerToolType]
public static class GetTeamTool
{
    [McpServerTool, Description("Get a specific team by ID.")]
    public static async Task<string> GetTeam(
        [Description("ID of the team to retrieve")]string teamId,
        HeroClient client)
    {
        if (string.IsNullOrEmpty(teamId))
            return "Error: Team ID is required";

        try
        {
            if (!Guid.TryParse(teamId, out Guid teamGuid))
                return "Error: Invalid Team ID format";

            var team = await client.Api.Teams[teamGuid].GetAsync();

            if (team == null)
                return $"No team found with ID {teamId}";

            return JsonSerializer.Serialize(team);
        }
        catch (Exception ex)
        {
            return $"Error retrieving team: {ex.Message}";
        }
    }
}
