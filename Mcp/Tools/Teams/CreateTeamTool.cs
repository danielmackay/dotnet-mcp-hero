using ApiSdk;
using ApiSdk.Models;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace HeroMcp.Tools.Teams;

[McpServerToolType]
public static class CreateTeamTool
{
    [McpServerTool, Description("Create a new team.")]
    public static async Task<string> CreateTeam(
        [Description("name of the team")]string name,
        HeroClient client,
        IMcpServer server)
    {
        if (string.IsNullOrEmpty(name))
            return "Error: Team name is required";

        try
        {
            var command = new CreateTeamCommand
            {
                Name = name
            };

            await client.Api.Teams.PostAsync(command);
            return $"Team '{name}' successfully created!";
        }
        catch (Exception ex)
        {
            return $"Error creating team: {ex.Message}";
        }
    }
}
