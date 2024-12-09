<Query Kind="Statements">
  <Reference Relative="09 input.txt">C:\Drive\Challenges\AoC 2024\09 input.txt</Reference>
</Query>

var input = "2333133121414131402";

input = File.ReadAllText("09 input.txt").Trim();

var blocks = Enumerable.Range(0, input.Length).SelectMany(i => Enumerable.Repeat(i % 2 == 0 ? (i / 2) : -1, input[i] - '0')).ToArray();

var buffer = blocks.ToArray();

long Answer(int[] value) => Enumerable.Range(0, value.Length).Zip(value).Where(x => x.Second > 0).Sum(x => x.First * (long)x.Second);

for (var (left, right) = (0, buffer.Length - 1); left < buffer.Length; left++)
{
  if (buffer[left] == -1)
  {
    while (right > 0 && buffer[right] == -1) { right--; }
    if (right < left) { break; }
    (buffer[left], buffer[right]) = (buffer[right], buffer[left]);
  }
}

Answer(buffer).Dump("Answer 1");

var used = Enumerable.Range(0, input.Length).ToLookup(x => x % 2, x => (input[x] - '0', x % 2 == 0 ? x / 2 : -1))[0].ToDictionary(x => x.Item2, x => x.Item1);
var lookup = Enumerable.Range(0, blocks.Length).ToLookup(x => blocks[x], x => x);

foreach (var (left, len1) in from n in Enumerable.Range(0, blocks[blocks.Length - 1] + 1).Reverse() select (lookup[n].First(), used[n]))
{
  // Return pos of a free spot (-1) of at least length len1
  var len2 = 0;
  if (Enumerable.Range(0, blocks.Length).Select(x => blocks[x] == -1 ? (++len2 == len1 ? x - len1 + 1 : 0) : (len2 = 0)).First(x => x > 0) is var right && right < left)
  {
    // Swap len1 blocks
    Enumerable.Range(0, len1).ToList().ForEach(i => (blocks[right + i], blocks[left + i]) = (blocks[left + i], blocks[right + i]));
  }
}

Answer(blocks).Dump("Answer 2");