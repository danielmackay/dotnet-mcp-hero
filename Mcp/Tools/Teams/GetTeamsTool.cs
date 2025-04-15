using ApiSdk;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;

namespace HeroMcp.Tools.Teams;

[McpServerToolType]
public static class GetTeamsTool
{
    [McpServerTool, Description("Get all teams from the API.")]
    public static async Task<string> GetTeams(HeroClient client)
    {
        try
        {
            var teams = await client.Api.Teams.GetAsync();

            if (teams == null || teams.Count == 0)
                return "No teams found.";

            return JsonSerializer.Serialize(teams);
        }
        catch (Exception ex)
        {
            return $"Error retrieving teams: {ex.Message}";
        }
    }
}
