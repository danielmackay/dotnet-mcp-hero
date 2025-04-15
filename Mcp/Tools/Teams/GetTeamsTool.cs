using ModelContextProtocol.Server;
using System.ComponentModel;
using ApiSdk;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace HeroMcp.Tools;

[McpServerToolType]
public static class GetTeamsTool
{
    [McpServerTool, Description("Get all teams from the API.")]
    public static async Task<string> GetTeams(HeroApi api)
    {
        try
        {
            var teams = await api.Api.Teams.GetAsync();

            if (teams == null || !teams.Any())
                return "No teams found.";

            return JsonSerializer.Serialize(teams);
        }
        catch (Exception ex)
        {
            return $"Error retrieving teams: {ex.Message}";
        }
    }
}