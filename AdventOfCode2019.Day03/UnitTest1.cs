using AdventOfCode2019.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2019.Day03
{
	public class UnitTest1
	{
		[Theory]
		[InlineData("R8,U5,L5,D3", "U7,R6,D4,L4", 6)]
		[InlineData("R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83", 159)]
		[InlineData("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 135)]
		public void WalkTests(string path1Csv, string path2Csv, int expected)
		{
			var points = new List<Point>();

			points.AddRange(Walk(path1Csv.Split(',')));
			points.AddRange(Walk(path2Csv.Split(',')));

			var crossings = from p in points
							where !p.IsEmpty
							group p by p into g
							where g.Count() > 1
							select g.Key;

			var closest = (from p in crossings
						   select Math.Abs(p.X) + Math.Abs(p.Y)
						  ).Min();

			Assert.Equal(expected, closest);
		}

		[Theory]
		[InlineData("input.txt", 2_180)]
		public async Task SolvePart1(string fileName, int expected)
		{
			var lines = Helpers.ReadLinesAsync(fileName);
			var points = new List<Point>();

			await foreach (var line in lines)
			{
				var path = line.Split(',');
				points.AddRange(Walk(path));
			}

			var crossings = from p in points
							where !p.IsEmpty
							group p by p into g
							where g.Count() > 1
							select g.Key;

			var closest = (from p in crossings
						   select Math.Abs(p.X) + Math.Abs(p.Y)
						  ).Min();

			Assert.Equal(expected, closest);
		}

		[Theory]
		[InlineData("R8,U5,L5,D3", "U7,R6,D4,L4", 30)]
		[InlineData("R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83", 610)]
		[InlineData("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 410)]
		public void ShortestIntersectionTests(string path1Csv, string path2Csv, int expected)
		{
			var path1 = Walk(path1Csv.Split(','));
			var path2 = Walk(path2Csv.Split(','));

			var actual = ShortestIntersection(path1, path2);

			Assert.Equal(expected, actual);
		}

		private static int ShortestIntersection(IEnumerable<Point> first, IEnumerable<Point> second)
		{
			var index = 0;
			var shortest = int.MaxValue;
			var list = first.ToList();

			foreach (var point in second)
			{
				if (!point.IsEmpty)
				{
					for (var a = 0; a < list.Count; a++)
					{
						if (shortest < index + a) continue;
						if (point != list[a]) continue;

						shortest = index + a;
					}
				}

				index++;
			}

			return shortest;
		}

		[Theory]
		[InlineData("input.txt", 112_316)]
		public async Task SolvePart2(string fileName, int expected)
		{
			var lines = Helpers.ReadLinesAsync(fileName);
			var csves = new List<IList<string>>();

			await foreach(var line in lines)
			{
				var values = line.Split(',');
				csves.Add(values);
			}

			var first = Walk(csves[0]);
			var second = Walk(csves[1]);

			var actual = ShortestIntersection(first, second);

			Assert.Equal(expected, actual);
		}

		private static IEnumerable<Point> Walk(IEnumerable<string> path)
		{
			var curr = new Point(x: 0, y: 0);
			yield return curr;

			foreach (var line in path)
			{
				var distance = int.Parse(line[1..]);

				for (var a = 0; a < distance; a++)
				{
					var _ = line[0] switch
					{
						'D' => curr.Y--,
						'L' => curr.X--,
						'R' => curr.X++,
						'U' => curr.Y++,
						_ => throw new ArgumentOutOfRangeException(nameof(line), line),
					};

					yield return curr;
				}
			}
		}
	}
}
