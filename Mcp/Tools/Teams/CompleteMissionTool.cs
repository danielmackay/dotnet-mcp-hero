using ApiSdk;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace HeroMcp.Tools.Teams;

[McpServerToolType]
public static class CompleteMissionTool
{
    [McpServerTool, Description("Complete a mission with a team.")]
    public static async Task<string> CompleteMission(
        [Description("ID of the team")]string teamId,
        HeroClient client)
    {
        if (string.IsNullOrEmpty(teamId))
            return "Error: Team ID is required";

        try
        {
            if (!Guid.TryParse(teamId, out Guid teamGuid))
                return "Error: Invalid Team ID format";

            // Send the request to complete the mission
            await client.Api.Teams[teamGuid].CompleteMission.PostAsync();

            return $"Mission completed successfully by team {teamId}";
        }
        catch (Exception ex)
        {
            return $"Error completing mission: {ex.Message}";
        }
    }
}
