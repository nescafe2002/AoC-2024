<Query Kind="Statements">
  <Reference Relative="05 input.txt">C:\Drive\Challenges\AoC 2024\05 input.txt</Reference>
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

var input = @"47|53
97|13
97|61
97|47
75|29
61|13
75|53
29|13
97|29
53|29
61|53
97|53
61|29
47|13
75|47
97|75
47|61
75|61
47|29
75|13
53|13

75,47,61,53,29
97,61,53,29,13
75,29,13
75,97,47,61,53
61,13,29
97,13,75,29,47".Split("\r\n");

input = File.ReadAllLines("05 input.txt");

var rules = input.TakeWhile(x => !string.IsNullOrEmpty(x)).Select(x => x.Split('|')).ToArray();
var updates = input.Skip(rules.Length + 1).Select(x => x.Split(',')).ToArray();

var pairs = rules.Select(x => (x[0], x[1])).ToHashSet();

int Answer(string[][] u) => u.Select(x => int.Parse(x[x.Length / 2])).Sum();

var correct = updates.Where(x => x.Window(2).All(y => !pairs.Contains((y.Last(), y.First())))).ToArray();

Answer(correct).Dump("Answer 1");

var comparer = Comparer<string>.Create((a, b) => pairs.Contains((a, b)) ? -1 : pairs.Contains((b, a)) ? 1 : 0);

Answer(updates.Except(correct).Select(x => x.OrderBy(x => x, comparer).ToArray()).ToArray()).Dump("Answer 2");