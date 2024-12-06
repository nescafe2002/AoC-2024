<Query Kind="Statements">
  <Reference Relative="06 input.txt">C:\Drive\Challenges\AoC 2024\06 input.txt</Reference>
</Query>

var input = @"....#.....
.........#
..........
..#.......
.......#..
..........
.#..^.....
........#.
#.........
......#...".Split("\r\n");

input = File.ReadAllLines("06 input.txt");

var map = Enumerable.Range(0, input.Length).SelectMany(y => Enumerable.Range(0, input[0].Length).Select(x => (x, y))).ToLookup(x => input[x.y][x.x], x => (x.x, x.y));

var floor = map['.'].Concat(map['^']).ToHashSet();
var start = map['^'].First();
var obstructions = map['#'].ToHashSet();

var dirs = new (int x, int y)[] { (0, -1), (1, 0), (0, 1), (-1, 0) };

var dir = 0;
var route = new HashSet<(int, int)>() { start };

for (var guard = start; ;)
{
  var newpos = (x: guard.x + dirs[dir].x, y: guard.y + dirs[dir].y);

  if (obstructions.Contains(newpos))
  {
    dir = (dir + 1) % 4;
    continue;
  }

  if (newpos.x >= 0 && newpos.y >= 0 && newpos.x < input[0].Length && newpos.y < input.Length)
  {
    route.Add(newpos);
    guard = newpos;
    continue;
  }

  break;
}

route.Count.Dump("Answer 1");

var loop = 0;
var path = new HashSet<(int, (int, int))>() { (0, start) };

foreach (var option in route.Except(new[] { start }))
{
  dir = 0;
  path.Clear();

  for (var guard = start; ;)
  {
    var newpos = (x: guard.x + dirs[dir].x, y: guard.y + dirs[dir].y);

    if (obstructions.Contains(newpos) || newpos == option)
    {
      dir = (dir + 1) % 4;
      continue;
    }

    if (!path.Add((dir, newpos)))
    {
      loop++;
      break;
    }

    if (newpos.x >= 0 && newpos.y >= 0 && newpos.x < input[0].Length && newpos.y < input.Length)
    {
      guard = newpos;
      continue;
    }

    break;
  }
}

loop.Dump("Answer 2");