<Query Kind="Statements">
  <Reference Relative="01 input.txt">C:\Drive\Challenges\AoC 2024\01 input.txt</Reference>
</Query>

var input = @"3   4
4   3
2   5
1   3
3   9
3   3".Split("\r\n");

input = File.ReadAllLines("01 input.txt");

var data = input.Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()).ToList();

var result = data.Select(x => x[0]).OrderBy(x => x).Zip(data.Select(x => x[1]).OrderBy(x => x)).Sum(x => Math.Abs(x.Second - x.First)).Dump("Answer 1");

data.Select(x => x[0]).GroupBy(x => x).Join(data.Select(x => x[1]).GroupBy(x => x), x => x.Key, x => x.Key, (x, y) => x.Key * x.Count() * y.Count()).Sum().Dump("Answer 2");