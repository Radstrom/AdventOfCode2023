using System.Text.RegularExpressions;

namespace Day5.Dirty;

public class Second
{
    public ulong Solve(string input)
    {
        var sections = input.Split("\n\n");

        var seeds = new Regex(@"(\d+ \d+)");

        var seedToSoilMap = ParseSection(sections[1]);
        var soilToFertilizerMap = ParseSection(sections[2]);
        var fertilizerToWaterMap = ParseSection(sections[3]);
        var waterToLightMap = ParseSection(sections[4]);
        var lightToTemperatureMap = ParseSection(sections[5]);
        var temperatureToHumidityMap = ParseSection(sections[6]);
        var humidityToLocationMap = ParseSection(sections[7]);

        var seedRanges = new List<Range>();
        foreach (Match seedPair in seeds.Matches(sections[0]))
        {
            var splitPair = seedPair.Value.Split(" ").Select(ulong.Parse).ToArray();
            seedRanges.Add(new Range
            {
                From = splitPair[0],
                Tooo = splitPair[0] + splitPair[1]-1
            });
        }

        foreach (var seedRange in seedRanges)
        {
            seedRange.Verify();
        }
        
        ulong lowestFoundValue = 0;
        for (ulong i = 0; i < ulong.MaxValue; i++)
        {
            var humidity = humidityToLocationMap.MapBackwards(i);
            var temperature = temperatureToHumidityMap.MapBackwards(humidity);
            var light = lightToTemperatureMap.MapBackwards(temperature);
            var water = waterToLightMap.MapBackwards(light);
            var fertilizer = fertilizerToWaterMap.MapBackwards(water);
            var soil = soilToFertilizerMap.MapBackwards(fertilizer);
            var seed = seedToSoilMap.MapBackwards(soil);

            if (seedRanges.Exists(x => x.IsInRange(seed)))
            {
                Console.WriteLine(lowestFoundValue + " => " + i);
                lowestFoundValue = i;
                break;
            }
        }

        return lowestFoundValue;
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
                    Tooo = sourceStart + length-1
                },
                Destination = new Range
                {
                    From = destinationStart,
                    Tooo = destinationStart + length-1
                }
            });
        }

        return newSection;
    }

    public class Section
    {
        public IList<Mapping> Mappings { get; init; } = new List<Mapping>();
        
        public ulong MapBackwards(ulong source)
        {
            var mapping = Mappings.FirstOrDefault(x => x.Destination.IsInRange(source));

            if (mapping is not null)
            {
                return mapping.Source.From + (source - mapping.Destination.From);
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
        public ulong Tooo { get; init; }

        public bool IsInRange(ulong num)
        {
            return num >= From && num <= Tooo;
        }

        public void Verify()
        {
            if(From >= Tooo)
            {
                throw new Exception();
            }
        }
    }
}