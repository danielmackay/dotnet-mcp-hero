using ApiSdk;
using ApiSdk.Models;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace HeroMcp.Tools.Heroes;

[McpServerToolType]
public static class CreateHeroTool
{
    [McpServerTool, Description("Create a new hero.")]
    public static async Task<string> CreateHero(
        [Description("hero name")]string name,
        [Description("hero alias")]string alias,
        [Description("power name")]string powerName,
        [Description("power level")]int powerLevel,
        HeroClient client)
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

            await client.Api.Heroes.PostAsync(command);
            return $"Hero {name} ({alias}) successfully created with power level {powerLevel}";
        }
        catch (Exception ex)
        {
            return $"Error creating hero: {ex.Message}";
        }
    }
}
