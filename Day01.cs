namespace advent_of_code_2022;

public static class Day01
{
    public const string TestFileName = "Day01_test";
    public const string ProductionFileName = "Day01";

    public static int Part1(List<string> input)
    {
        var list1 = new List<int>();
        var list2 = new List<int>();

        foreach (var riga in input)
        {
            var numeri = riga.Split([' '], StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            list1.Add(numeri[0]);
            list2.Add(numeri[1]);
        }

        list1 = list1.Order().ToList();    
        list2 = list2.Order().ToList();    
        
        var sommaDistanze = 0;
        for (var i = 0; i < list1.Count; i++)
        {
            sommaDistanze += Math.Abs(list1[i] - list2[i]);
        }

        return sommaDistanze;
    }

    public static int Part2(List<string> input)
    {
        var list1 = new List<int>();
        var list2 = new List<int>();

        foreach (var riga in input)
        {
            var numeri = riga.Split([' '], StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            list1.Add(numeri[0]);
            list2.Add(numeri[1]);
        }

        var similarità = list2.GroupBy(it => it).ToDictionary(it => it.Key, it => it.Count());
        
        return list1.Sum(it => it * (similarità.GetValueOrDefault(it, 0)));
        
    }
}