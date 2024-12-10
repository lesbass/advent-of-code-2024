namespace advent_of_code_2024;

public static class Day05
{
    public const string TestFileName = "Day05_test";
    public const string ProductionFileName = "Day05";

    private static Dictionary<int, int> GetUpdate(string row)
    {
        return row.Split(",").Select(int.Parse).Select((it, index) => (it, index))
            .ToDictionary(it => it.it, it => it.index);
    }

    private static int GetMiddleItemForValidUpdate(Dictionary<int, int> update, List<string> rules)
    {
        foreach (var rule in rules)
        {
            var ruleSplit = rule.Split("|").Select(int.Parse).ToList();
            var prev = ruleSplit.First();
            var next = ruleSplit.Last();
            if (!update.ContainsKey(prev) || !update.ContainsKey(next)) continue;
            if (update[prev] > update[next]) return 0;
        }

        return update.OrderBy(it => it.Value).ToList()[update.Count / 2].Key;
    }

    public static int Part1(List<string> input)
    {
        var indexOfEmptyLine = 0;
        for (var i = 0; i < input.Count; i++)
            if (string.IsNullOrWhiteSpace(input[i]))
            {
                indexOfEmptyLine = i;
                break;
            }

        var rules = input.Take(indexOfEmptyLine).ToList();
        var updates = input.Skip(indexOfEmptyLine + 1).Select(GetUpdate).ToList();

        return updates.Sum(it => GetMiddleItemForValidUpdate(it, rules));
    }

    private static IEnumerable<string> GetApplicableRules(Dictionary<int, int> update, List<string> rules)
    {
        foreach (var rule in rules)
        {
            var ruleSplit = rule.Split("|").Select(int.Parse).ToList();
            var prev = ruleSplit.First();
            var next = ruleSplit.Last();
            if (!update.ContainsKey(prev) || !update.ContainsKey(next)) continue;
            yield return rule;
        }
    }

    private static Dictionary<int, int> FixIncorrectUpdate(Dictionary<int, int> update, List<string> rules)
    {
        var applicableRules = GetApplicableRules(update, rules).ToList();
        var modified = false;
        foreach (var rule in applicableRules)
        {
            var ruleSplit = rule.Split("|").Select(int.Parse).ToList();
            var prev = ruleSplit.First();
            var next = ruleSplit.Last();
            if (update[prev] > update[next])
            {
                var prevIndex = update[prev];
                var nextIndex = update[next];
                update[prev] = nextIndex;
                update[next] = prevIndex;
                modified = true;
                break;
            }
        }

        if (!modified) return update;
        return FixIncorrectUpdate(update, rules);
    }

    public static int Part2(List<string> input)
    {
        var indexOfEmptyLine = 0;
        for (var i = 0; i < input.Count; i++)
            if (string.IsNullOrWhiteSpace(input[i]))
            {
                indexOfEmptyLine = i;
                break;
            }

        var rules = input.Take(indexOfEmptyLine).ToList();
        var updates = input.Skip(indexOfEmptyLine + 1).Select(GetUpdate).ToList();

        var incorrectUpdates = updates
            .Where(it => GetMiddleItemForValidUpdate(it, rules) == 0).ToList();

        return incorrectUpdates
            .Select(it => FixIncorrectUpdate(it, rules))
            .Sum(it => GetMiddleItemForValidUpdate(FixIncorrectUpdate(it, rules), rules));
    }
}