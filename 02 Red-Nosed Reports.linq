<Query Kind="Statements">
  <Reference Relative="02 input.txt">C:\Drive\Challenges\AoC 2024\02 input.txt</Reference>
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

var input = @"7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9".Split("\r\n");

input = File.ReadAllLines("02 input.txt");

var reports = input.Select(x => x.Split(' ').Select(int.Parse).ToArray());

var allowed = new[] { new[] { 1, 2, 3 }.ToHashSet(), new[] { -1, -2, -3 }.ToHashSet() };

reports.Count(x => allowed.Any(y => x.Window(2).All(z => y.Contains(z.Last() - z.First())))).Dump("Answer 1");

reports
  .Count(x =>
    Enumerable.Range(0, x.Count())
      .Select(i => x.Take(i).Concat(x.Skip(i + 1)))
      .Any(i => allowed.Any(y => i.Window(2).All(z => y.Contains(z.Last() - z.First()))))).Dump("Answer 2");