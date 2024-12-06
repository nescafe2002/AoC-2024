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
  var newpos = (guard.x + dirs[dir].x, guard.y + dirs[dir].y);

  if (obstructions.Contains(newpos))
  {
    dir = (dir + 1) % 4;
    continue;
  }

  if (floor.Contains(newpos))
  {
    route.Add(newpos);
    guard = newpos;
    continue;
  }

  break;
}

route.Count.Dump("Answer 1");

var o = 0;

foreach (var option in floor.Except(new[] { start }).ToList())
{
  dir = 0;
  var path = new HashSet<(int, (int, int))>() { (dir, start) };
  var found = false;

  route = new HashSet<(int, int)>() { start };

  for (var guard = start; !found;)
  {
    var newpos = (x: guard.x + dirs[dir].x, y: guard.y + dirs[dir].y);

    if (obstructions.Contains(newpos) || newpos == option)
    {
      dir = (dir + 1) % 4;
      continue;
    }

    if (!path.Add((dir, newpos)))
    {
      o++;
      found = true;
      break;
    }

    if (floor.Contains(newpos))
    {
      route.Add(newpos);
      guard = newpos;
      continue;
    }

    break;
  }
}

o.Dump("Answer 2");