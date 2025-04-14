using ModelContextProtocol.Server;
using System.ComponentModel;
using ApiSdk;
using ApiSdk.Models;
using ModelContextProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace HeroMcp.Tools;

[McpServerToolType]
public static class TeamTool
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

    [McpServerTool, Description("Execute a mission with a team.")]
    public static async Task<string> ExecuteMission(
        [Description("ID of the team")]string teamId,
        [Description("Description of the mission")]string missionDescription,
        HeroApi api)
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
            await api.Api.Teams[teamGuid].ExecuteMission.PostAsync(command);

            return $"Team {teamId} has been sent on mission: '{missionDescription}'";
        }
        catch (Exception ex)
        {
            return $"Error executing mission: {ex.Message}";
        }
    }

    [McpServerTool, Description("Complete a mission with a team.")]
    public static async Task<string> CompleteMission(
        [Description("ID of the team")]string teamId,
        HeroApi api)
    {
        if (string.IsNullOrEmpty(teamId))
            return "Error: Team ID is required";

        try
        {
            if (!Guid.TryParse(teamId, out Guid teamGuid))
                return "Error: Invalid Team ID format";

            // Send the request to complete the mission
            await api.Api.Teams[teamGuid].CompleteMission.PostAsync();

            return $"Mission completed successfully by team {teamId}";
        }
        catch (Exception ex)
        {
            return $"Error completing mission: {ex.Message}";
        }
    }

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
