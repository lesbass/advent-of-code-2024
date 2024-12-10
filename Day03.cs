namespace advent_of_code_2024;

public static class Day03
{
    public const string TestFileName = "Day03_test";
    public const string ProductionFileName = "Day03";

    private static bool IsValidMul(string mul)
    {
        if (mul.Count(it => it == ',') != 1) return false;
        if (mul.Contains(' ')) return false;
        var validChars = new List<char> { '(', ')', ',' };
        for (var i = 0; i < 10; i++) validChars.Add(i.ToString()[0]);
        return mul.All(it => validChars.Contains(it)) && mul.Split(',').All(it => int.TryParse(it, out _));
    }

    private static int ExecuteMul(string mul)
    {
        return mul.Split(',').Aggregate(1, (result, part) => result * int.Parse(part));
    }

    private static int GetResult(string row)
    {
        var muls = row.Split("mul(").Skip(1).SelectMany(it => it.Split(')'));
        var validMuls = muls.Where(IsValidMul).ToList();
        return validMuls.Sum(ExecuteMul);
    }

    private static string CleanUpRow(string row)
    {
        var byDont = row.Split("don't()");

        return byDont.Skip(1)
            .Select(parts => parts.Split("do()"))
            .Where(byDo => byDo.Length > 1)
            .Aggregate(byDont[0], (current, byDo) =>
                current + string.Join("", byDo.Skip(1)));
    }

    public static int Part1(List<string> input)
    {
        var row = string.Join("", input);
        return GetResult(row);
    }

    public static int Part2(List<string> input)
    {
        var row = string.Join("", input);
        return GetResult(CleanUpRow(row));
    }
}