<Query Kind="Statements">
  <Reference Relative="03 input.txt">C:\Drive\Challenges\AoC 2024\03 input.txt</Reference>
</Query>

var input = @"xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";

input = File.ReadAllText("03 input.txt");

var re = new Regex(@"mul\((\d+),(\d+)\)");

int Mul(string s) => re.Matches(s).Select(x => x.Groups.Cast<Group>().Skip(1).Select(x => int.Parse(x.Value)).Aggregate((x, y) => x * y)).Sum();

Mul(input).Dump("Answer 1");

//input = @"xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";

var enabled = true;

Mul(Regex.Replace(input, @"don't\(\)|do\(\)|.", m => (enabled = m.Value == "do()" ? true : m.Value == "don't()" ? false : enabled) ? m.Value : "")).Dump("Answer 2");