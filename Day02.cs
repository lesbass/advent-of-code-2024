namespace advent_of_code_2022;

public record Report
{
    public Report(string rawData)
    {
        Data = rawData.Split(' ').Select(int.Parse).ToList();
    }

    private List<int> Data { get; }

    public bool IsSafe()
    {
        return CheckSafety(Data);
    }

    public bool IsSafelySafe()
    {
        for (var i = 0; i < Data.Count; i++)
        {
            var copy = new List<int>(Data);
            copy.RemoveAt(i);
            if (CheckSafety(copy)) return true;
        }

        return false;
    }

    private bool CheckSafety(List<int> data)
    {
        var ascSorted = data.OrderBy(it => it).ToList();
        var descSorted = data.OrderByDescending(it => it).ToList();

        if (!data.SequenceEqual(ascSorted) && !data.SequenceEqual(descSorted)) return false;

        for (var i = 1; i < data.Count; i++)
        {
            var delta = Math.Abs(data[i] - data[i - 1]);
            if (delta is < 1 or > 3) return false;
        }

        return true;
    }
}

public static class Day02
{
    public const string TestFileName = "Day02_test";
    public const string ProductionFileName = "Day02";

    public static int Part1(List<string> input)
    {
        var reports = input.Select(it => new Report(it)).ToList();
        return reports.Count(it => it.IsSafe());
    }

    public static int Part2(List<string> input)
    {
        var reports = input.Select(it => new Report(it)).ToList();
        return reports.Count(it => it.IsSafelySafe());
    }
}