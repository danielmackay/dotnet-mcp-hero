using ApiSdk;
using ApiSdk.Models;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace HeroMcp.Tools.Teams;

[McpServerToolType]
public static class ExecuteMissionTool
{
    [McpServerTool, Description("Execute a mission with a team.")]
    public static async Task<string> ExecuteMission(
        [Description("ID of the team")]string teamId,
        [Description("Description of the mission")]string missionDescription,
        HeroClient client)
    {
        if (string.IsNullOrEmpty(teamId))
            return "Error: Team ID is required";

        if (string.IsNullOrEmpty(missionDescription))
            return "Error: Mission description is required";

        try
        {
            if (!Guid.TryParse(teamId, out Guid teamGuid))
                return "Error: Invalid Team ID format";

            // Create the execute mission command with the description
            var command = new ExecuteMissionCommand
            {
                Description = missionDescription
            };

            // Send the request to the server
            await client.Api.Teams[teamGuid].ExecuteMission.PostAsync(command);

            return $"Team {teamId} has been sent on mission: '{missionDescription}'";
        }
        catch (Exception ex)
        {
            return $"Error executing mission: {ex.Message}";
        }
    }
}
