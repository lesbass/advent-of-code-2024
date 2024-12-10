using dotenv.net;
using Xunit;
using static advent_of_code_2024.Day06;

namespace advent_of_code_2024.tests;

public class Day06Tests
{
    public Day06Tests()
    {
        DotEnv.Load();
    }

    [Fact]
    public void Part1_Test()
    {
        var input = Utils.ReadInput(ProductionFileName);

        Assert.Equal(Utils.GetResult("DAY06_PART1"), Part1(input));
    }

    [Fact]
    public void Part2_Test()
    {
        var input = Utils.ReadInput(ProductionFileName);

        Assert.Equal(Utils.GetResult("DAY06_PART2"), Part2(input));
    }
}