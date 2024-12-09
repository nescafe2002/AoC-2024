<Query Kind="Statements">
  <Reference Relative="09 input.txt">C:\Drive\Challenges\AoC 2024\09 input.txt</Reference>
</Query>

var input = "2333133121414131402";

input = File.ReadAllText("09 input.txt").Trim();

var blocks = Enumerable.Range(0, input.Length).SelectMany(i => Enumerable.Repeat(i % 2 == 0 ? (i / 2) : -1, input[i] - '0')).ToArray();

var j = blocks.Length - 1;

for (var i = 0; i < blocks.Length; i++)
{
  if (blocks[i] == -1)
  {
    while (j > 0 && blocks[j] == -1) { j--; }
    if (j < i) { break; }
    (blocks[i], blocks[j]) = (blocks[j], blocks[i]);
  }
}

Enumerable.Range(0, blocks.Length).TakeWhile(x => blocks[x] >= 0).Sum(x => x * (long)blocks[x]).Dump("Answer 1");

blocks = Enumerable.Range(0, input.Length).SelectMany(i => Enumerable.Repeat(i % 2 == 0 ? (i / 2) : -1, input[i] - '0')).ToArray();
var slots = Enumerable.Range(0, input.Length).ToLookup(x => x % 2, x => (input[x] - '0', x % 2 == 0 ? x / 2 : -1));
var used = slots[0];

for (var n = blocks.Max(x => x); n > 0; n--)
{
  var pos1 = Enumerable.Range(0, blocks.Length).First(x => blocks[x] == n);

  var len = used.First(x => x.Item2 == n).Item1;

  var len2 = 0;
  var pos2 = -1;
  for (int i = 0; i < blocks.Length; i++)
  {
    if (blocks[i] == -1)
    {
      len2++;
      if (len2 == len)
      {
        pos2 = i - len + 1;
        break;
      }
    }
    else len2 = 0;
  }

  if (pos2 >= 0 && pos2 < pos1)
  {
    for (var i = 0; i < len; i++)
    {
      (blocks[pos2 + i], blocks[pos1 + i]) = (blocks[pos1 + i], blocks[pos2 + i]);
    }
  }
}

Enumerable.Range(0, blocks.Length).Where(x => blocks[x] >= 0).Sum(x => x * (long)blocks[x]).Dump("Answer 2");
