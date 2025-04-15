using ModelContextProtocol.Server;
using System.ComponentModel;
using ApiSdk;
using ApiSdk.Models;
using ModelContextProtocol;
using System;
using System.Threading.Tasks;

namespace HeroMcp.Tools;

[McpServerToolType]
public static class CreateTeamTool
{
    [McpServerTool, Description("Create a new team.")]
    public static async Task<string> CreateTeam(
        [Description("name of the team")]string name,
        HeroApi api,
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

            await api.Api.Teams.PostAsync(command);
            return $"Team '{name}' successfully created!";
        }
        catch (Exception ex)
        {
            return $"Error creating team: {ex.Message}";
        }
    }
}