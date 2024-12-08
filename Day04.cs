namespace advent_of_code_2022;

public static class Day04
{
    public const string TestFileName = "Day04_test";
    public const string ProductionFileName = "Day04";

    private const string WordToSearch = "XMAS";

    private static bool Check123(List<string> input, int y, int x, int c)
    {
        if (y < 0 || x < 0 || y >= input.Count || x >= input[0].Length) return false;
        return input[y][x] == WordToSearch[c];
    }

    private static int FindE(List<string> input, Position startPosition)
    {
        for (var c = 1; c < WordToSearch.Length; c++)
            if (!Check123(input, startPosition.Y, startPosition.X + c, c))
                return 0;
        return 1;
    }

    private static int FindNE(List<string> input, Position startPosition)
    {
        for (var c = 1; c < WordToSearch.Length; c++)
            if (!Check123(input, startPosition.Y - c, startPosition.X + c, c))
                return 0;
        return 1;
    }

    private static int FindNW(List<string> input, Position startPosition)
    {
        for (var c = 1; c < WordToSearch.Length; c++)
            if (!Check123(input, startPosition.Y - c, startPosition.X - c, c))
                return 0;
        return 1;
    }

    private static int FindSE(List<string> input, Position startPosition)
    {
        for (var c = 1; c < WordToSearch.Length; c++)
            if (!Check123(input, startPosition.Y + c, startPosition.X + c, c))
                return 0;
        return 1;
    }

    private static int FindSW(List<string> input, Position startPosition)
    {
        for (var c = 1; c < WordToSearch.Length; c++)
            if (!Check123(input, startPosition.Y + c, startPosition.X - c, c))
                return 0;
        return 1;
    }

    private static int FindW(List<string> input, Position startPosition)
    {
        for (var c = 1; c < WordToSearch.Length; c++)
            if (!Check123(input, startPosition.Y, startPosition.X - c, c))
                return 0;
        return 1;
    }

    private static int FindN(List<string> input, Position startPosition)
    {
        for (var c = 1; c < WordToSearch.Length; c++)
            if (!Check123(input, startPosition.Y - c, startPosition.X, c))
                return 0;
        return 1;
    }

    private static int FindS(List<string> input, Position startPosition)
    {
        for (var c = 1; c < WordToSearch.Length; c++)
            if (!Check123(input, startPosition.Y + c, startPosition.X, c))
                return 0;
        return 1;
    }

    private static int CheckPosition(List<string> input, Position startPosition)
    {
        return FindE(input, startPosition) +
               FindW(input, startPosition) +
               FindN(input, startPosition) +
               FindS(input, startPosition) +
               FindNE(input, startPosition) +
               FindNW(input, startPosition) +
               FindSE(input, startPosition) +
               FindSW(input, startPosition);
    }

    public static int Part1(List<string> input)
    {
        var allStarts = new List<Position>();
        for (var y = 0; y < input.Count; y++)
        {
            var line = input[y];
            for (var x = 0; x < line.Length; x++)
                if (line[x] == WordToSearch[0])
                    allStarts.Add(new Position(x, y));
        }

        return allStarts.Sum(it => CheckPosition(input, it));
    }

    private static int IsMas(List<string> input, Position position)
    {
        var result = 0;
        try
        {
            if (input[position.Y - 1][position.X - 1] == 'M' && input[position.Y + 1][position.X - 1] == 'M' &&
                input[position.Y - 1][position.X + 1] == 'S' && input[position.Y + 1][position.X + 1] == 'S') result++;
        }
        catch
        {
            // ignored
        }

        try
        {
            if (input[position.Y - 1][position.X - 1] == 'S' && input[position.Y + 1][position.X - 1] == 'S' &&
                input[position.Y - 1][position.X + 1] == 'M' && input[position.Y + 1][position.X + 1] == 'M') result++;
        }
        catch
        {
            // ignored
        }

        try
        {
            if (input[position.Y - 1][position.X - 1] == 'M' && input[position.Y + 1][position.X - 1] == 'S' &&
                input[position.Y - 1][position.X + 1] == 'M' && input[position.Y + 1][position.X + 1] == 'S') result++;
        }
        catch
        {
            // ignored
        }

        try
        {
            if (input[position.Y - 1][position.X - 1] == 'S' && input[position.Y + 1][position.X - 1] == 'M' &&
                input[position.Y - 1][position.X + 1] == 'S' && input[position.Y + 1][position.X + 1] == 'M') result++;
        }
        catch
        {
            // ignored
        }

        return result;
    }

    public static int Part2(List<string> input)
    {
        var allCenters = new List<Position>();
        for (var y = 0; y < input.Count; y++)
        {
            var line = input[y];
            for (var x = 0; x < line.Length; x++)
                if (line[x] == 'A')
                    allCenters.Add(new Position(x, y));
        }

        return allCenters.Sum(it => IsMas(input, it));
    }
}

public record Position(int X, int Y);