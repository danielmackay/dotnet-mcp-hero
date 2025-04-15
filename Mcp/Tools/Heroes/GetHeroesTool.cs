using ModelContextProtocol.Server;
using System.ComponentModel;
using ApiSdk;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace HeroMcp.Tools;

[McpServerToolType]
public static class GetHeroesTool
{
    [McpServerTool, Description("Get all heroes from the API.")]
    public static async Task<string> GetHeroes(HeroApi api)
    {
        try
        {
            var heroes = await api.Api.Heroes.GetAsync();

            if (heroes == null || !heroes.Any())
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
