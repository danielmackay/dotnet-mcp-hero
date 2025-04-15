using ModelContextProtocol.Server;
using System.ComponentModel;
using ApiSdk;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace HeroMcp.Tools;

[McpServerToolType]
public static class GetTeamTool
{
    [McpServerTool, Description("Get a specific team by ID.")]
    public static async Task<string> GetTeam(
        [Description("ID of the team to retrieve")]string teamId,
        HeroApi api)
    {
        if (string.IsNullOrEmpty(teamId))
            return "Error: Team ID is required";

        try
        {
            if (!Guid.TryParse(teamId, out Guid teamGuid))
                return "Error: Invalid Team ID format";

            var team = await api.Api.Teams[teamGuid].GetAsync();

            if (team == null)
                return $"No team found with ID {teamId}";

            return JsonSerializer.Serialize(team, new JsonSerializerOptions
            {
                WriteIndented = true
            });
        }
        catch (Exception ex)
        {
            return $"Error retrieving team: {ex.Message}";
        }
    }
}