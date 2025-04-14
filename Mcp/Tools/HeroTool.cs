using ModelContextProtocol.Server;
using System.ComponentModel;
using ApiSdk;
using ApiSdk.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModelContextProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace HeroMcp.Tools;

[McpServerToolType]
public static class HeroTool
{
    [McpServerTool, Description("Get all heroes from the API.")]
    public static async Task<string> GetHeroes(HeroApi api)
    {
        try
        {
            var heroes = await api.Api.Heroes.GetAsync();

            if (heroes == null || !heroes.Any())
                return "No heroes found.";

            return JsonSerializer.Serialize(heroes);
        }
        catch (Exception ex)
        {
            return $"Error retrieving heroes: {ex.Message}";
        }
    }

    [McpServerTool, Description("Create a new hero.")]
    public static async Task<string> CreateHero(
        [Description("hero name")]string name,
        [Description("hero alias")]string alias,
        [Description("power name")]string powerName,
        [Description("power level")]int powerLevel,
        HeroApi api,
        IMcpServer server)
    {
        if (string.IsNullOrEmpty(name))
            return "Error: Hero name is required";

        if (string.IsNullOrEmpty(alias))
            return "Error: Hero alias is required";

        if (powerLevel < 1 || powerLevel > 10)
            return "Error: Power level must be between 1 and 10";

        try
        {
            var command = new CreateHeroCommand
            {
                Name = name,
                Alias = alias,
                Powers = [new CreateHeroPowerDto { Name = powerName, PowerLevel = powerLevel }]
            };

            await api.Api.Heroes.PostAsync(command);
            return $"Hero {name} ({alias}) successfully created with power level {powerLevel}";
        }
        catch (Exception ex)
        {
            return $"Error creating hero: {ex.Message}";
        }
    }
}
