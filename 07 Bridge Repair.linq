<Query Kind="Statements">
  <Reference Relative="07 input.txt">C:\Drive\Challenges\AoC 2024\07 input.txt</Reference>
</Query>

var input = @"190: 10 19
3267: 81 40 27
83: 17 5
156: 15 6
7290: 6 8 6 15
161011: 16 10 13
192: 17 8 14
21037: 9 7 18 13
292: 11 6 16 20".Split("\r\n");

input = File.ReadAllLines("07 input.txt").ToArray();

var lines = input.Select(x => x.Split(": ")).Select(x => (Answer: long.Parse(x[0]), Numbers: x[1].Split(' ').Select(long.Parse).ToArray())).ToArray();

long Answer(Func<IEnumerable<long>, long, IEnumerable<long>> agg)
  => lines
      .Where(line => line.Numbers.Skip(1).Reverse().Aggregate(new[] { line.Answer }.AsEnumerable(), (x, y) => agg(x, y).Where(x => x > 0)).Any(x => x == line.Numbers[0]))
      .Sum(x => x.Answer);

Answer((x, y) => x.SelectMany(z => new[] { y > 0 && (z % y) == 0 ? z / y : 0, z - y })).Dump("Answer 1");

Answer((x, y) => x.SelectMany(z => new[] { y > 0 && (z % y) == 0 ? z / y : 0, z - y, long.Parse(z != y && z.ToString().EndsWith(y.ToString()) ? z.ToString()[..((int)Math.Log10(z) - (int)Math.Log10(y))] : "0") })).Dump("Answer 2");