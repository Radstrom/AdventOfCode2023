using System.Text.RegularExpressions;

namespace Day5.Dirty;

public class First
{
    public ulong Solve(string input)
    {
        var sections = input.Split("\n\n");

        var seeds = new Regex("(\\d+)");

        var SeedToSoilMap = ParseSection(sections[1]);
        var SoilToFertilizerMap = ParseSection(sections[2]);
        var FertilizerToWaterMap = ParseSection(sections[3]);
        var WaterToLightMap = ParseSection(sections[4]);
        var LightToTemperatureMap = ParseSection(sections[5]);
        var TemperatureToHumidityMap = ParseSection(sections[6]);
        var HumidityToLocationMap = ParseSection(sections[7]);

        ulong lowestSeedNumber = 0;
        ulong lowestLocation = ulong.MaxValue;
        foreach (Match seed in seeds.Matches(sections[0]))
        {
            var soil = SeedToSoilMap.MapForward(ulong.Parse(seed.Value));
            var fertilizer = SoilToFertilizerMap.MapForward(soil);
            var water = FertilizerToWaterMap.MapForward(fertilizer);
            var light = WaterToLightMap.MapForward(water);
            var temperature = LightToTemperatureMap.MapForward(light);
            var humidity = TemperatureToHumidityMap.MapForward(temperature);
            var location = HumidityToLocationMap.MapForward(humidity);

            if (location < lowestLocation)
            {
                lowestSeedNumber = ulong.Parse(seed.Value);
                lowestLocation = location;
            }
        }

        return lowestLocation;
    }

    public Section ParseSection(string section)
    {
        var newSection = new Section();
        foreach (var line in section.Split('\n').Skip(1))
        {
            var splitLine = line.Split(" ");
            var destinationStart = ulong.Parse(splitLine[0]);
            var sourceStart = ulong.Parse(splitLine[1]);
            var length = ulong.Parse(splitLine[2]);
            
            newSection.Mappings.Add(new Mapping
            {
                Source = new Range
                {
                    From = sourceStart,
                    To = sourceStart + length
                },
                Destination = new Range
                {
                    From = destinationStart,
                    To = destinationStart + length
                }
            });
        }

        return newSection;
    }

    public class Section
    {
        public IList<Mapping> Mappings { get; init; } = new List<Mapping>();

        public ulong MapForward(ulong source)
        {
            foreach (var mapping in Mappings)
            {
                if (source > mapping.Source.From && source < mapping.Source.To)
                {
                    return mapping.Destination.From + (source - mapping.Source.From);
                }
            }

            return source;
        }
    }

    public class Mapping
    {
        public Range Source { get; init; }
        public Range Destination { get; init; }
    }

    public class Range
    {
        public ulong From { get; init; }
        public ulong To { get; init; }
    }
}

public static class SectionExtensions {
    
}