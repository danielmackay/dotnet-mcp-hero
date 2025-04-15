using ApiSdk;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;

namespace HeroMcp.Tools.Heroes;

[McpServerToolType]
public static class GetHeroesTool
{
    [McpServerTool, Description("Get all heroes from the API.")]
    public static async Task<string> GetHeroes(HeroClient client)
    {
        try
        {
            var heroes = await client.Api.Heroes.GetAsync();

            if (heroes == null || heroes.Count == 0)
                return "No heroes found.";

            // var dto = heroes.Select(h => new GetHeroDto(
            //     h.Id.Value,
            //     h.Name,
            //     h.Alias,
            //     h.Powers.Select(p => new HeroPowerDto(
            //         p.Name,
            //         p.PowerLevel
            //     )).ToList()
            // )).ToList();

            return JsonSerializer.Serialize(heroes);
        }
        catch (Exception ex)
        {
            return $"Error retrieving heroes: {ex.Message}";
        }
    }
}

// file record GetHeroDto(string Id, string Name, string Alias, List<HeroPowerDto> Powers);
// file record HeroPowerDto(string Name, int PowerLevel);
