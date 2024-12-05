<Query Kind="Statements">
  <Reference Relative="05 input.txt">C:\Drive\Challenges\AoC 2024\05 input.txt</Reference>
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
var table = rules.ToLookup(x => x[0], x => x[1]); // left - right

var correct = (
  from u in updates
  let seq = Enumerable.Range(0, u.Length).ToDictionary(i => u[i], i => i)
  where Enumerable.Range(0, u.Length).SelectMany(left => table[u[left]].Select(x => (left, x))).All(page => !seq.TryGetValue(page.x, out var right) || page.left < right)
  select u).ToArray();

correct.Select(x => int.Parse(x[x.Length / 2])).Sum().Dump("Answer 1");

var incorrect = updates.Except(correct).ToArray();

foreach (var update in incorrect)
{
  var lower = 0;

  for (var retry = true; retry;)
  {
    retry = false;

    var seq = Enumerable.Range(0, update.Length).ToDictionary(i => update[i], i => i);

    foreach (var page in Enumerable.Range(0, update.Length).Skip(lower).SelectMany(left => table[update[left]].Select(x => (left, x))))
    {
      if (seq.TryGetValue(page.x, out var right) && page.left > right)
      {
        var old = update[right];
        update[right] = update[page.left];
        update[page.left] = old;
        retry = true;
        lower = Math.Min(right, page.left);
        break;
      }
    }
  }
}

incorrect.Select(x => int.Parse(x[x.Length / 2])).Sum().Dump("Answer 2");