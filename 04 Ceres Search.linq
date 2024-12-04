<Query Kind="Statements">
  <Reference Relative="04 input.txt">C:\Drive\Challenges\AoC 2024\04 input.txt</Reference>
</Query>

var input = @"MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX".Split("\r\n");

input = File.ReadAllLines("04 input.txt");

var bitmap = Enumerable.Range(0, input.Length).SelectMany(x => Enumerable.Range(0, input[0].Length).Select(y => (x, y, input[x][y]))).ToLookup(x => x.Item3, x => (x.x, x.y));

var directions = new[] { (0, 1), (0, -1), (1, 0), (-1, 0), (1, 1), (1, -1), (-1, 1), (-1, -1) };

var word = "XMAS".ToCharArray();

bitmap[word[0]].Sum(o => directions.Count(d => Enumerable.Range(0, word.Length).Skip(1).All(i => bitmap[word[i]].Contains((o.x + d.Item1 * i, o.y + d.Item2 * i))))).Dump("Answer 1");

bitmap[word[2]].Count(o => directions.Skip(4).Count(d => Enumerable.Range(-1, 3).All(i => bitmap[word[i + 2]].Contains((o.x + d.Item1 * i, o.y + d.Item2 * i)))) == 2).Dump("Answer 2");
