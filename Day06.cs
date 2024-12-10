namespace advent_of_code_2024;

public static class Day06
{
    public const string TestFileName = "Day06_test";
    public const string ProductionFileName = "Day06";

    public static int Part1(List<string> input)
    {
        var positions = new List<Map.Position>();
        var rover = new Map.Rover(new Map.Position(0, 0), 0);
        for (var y = 0; y < input.Count; y++)
        {
            var row = input[y];
            for (var x = 0; x < row.Length; x++)
            {
                var c = row[x];
                positions.Add(new Map.Position(x, y, c == '#', c == '^'));
                if (c == '^') rover = new Map.Rover(new Map.Position(x, y), Direction.North);
            }
        }

        var map = new Map(positions, rover);

        while (!map.IsExiting()) map = map.MoveRover();
        return map.Positions.Count(it => it.IsVisited);
    }

    public static int Part2(List<string> input)
    {
        return 0;
    }
}

public record Map(List<Map.Position> Positions, Map.Rover MyRover)
{
    public bool IsExiting()
    {
        var exitingWest = MyRover.Position.X == 0 && MyRover.Direction == Direction.West;
        var exitingNorth = MyRover.Position.Y == 0 && MyRover.Direction == Direction.North;
        var exitingEast = MyRover.Position.X == Positions.Max(p => p.X) && MyRover.Direction == Direction.East;
        var exitingSouth = MyRover.Position.Y == Positions.Max(p => p.Y) && MyRover.Direction == Direction.South;
        return exitingNorth || exitingEast || exitingSouth || exitingWest;
    }

    public Map MoveRover()
    {
        var nextPosition = GetNextPosition();
        if (nextPosition.IsObstacle) return new Map(Positions, new Rover(MyRover.Position, RotateRight()));
        var newPositions = Positions.Select(it => it == nextPosition ? it with { IsVisited = true } : it).ToList();
        var myRover = new Rover(nextPosition, MyRover.Direction);
        return new Map(newPositions, myRover);
    }

    private Direction RotateRight()
    {
        return MyRover.Direction switch
        {
            Direction.North => Direction.East,
            Direction.East => Direction.South,
            Direction.South => Direction.West,
            Direction.West => Direction.North,
            _ => throw new NotImplementedException()
        };
    }

    private Position GetNextPosition()
    {
        var currentPosition = MyRover.Position;
        if (MyRover.Direction == Direction.North)
            return Positions.First(it => it.X == currentPosition.X && it.Y == currentPosition.Y - 1);
        if (MyRover.Direction == Direction.East)
            return Positions.First(it => it.X == currentPosition.X + 1 && it.Y == currentPosition.Y);
        if (MyRover.Direction == Direction.South)
            return Positions.First(it => it.X == currentPosition.X && it.Y == currentPosition.Y + 1);
        return Positions.First(it => it.X == currentPosition.X - 1 && it.Y == currentPosition.Y);
    }

    public record Position(int X, int Y, bool IsObstacle = false, bool IsVisited = false);

    public record Rover(Position Position, Direction Direction);
}

public enum Direction
{
    North = 0,
    East = 1,
    South = 2,
    West = 3
}