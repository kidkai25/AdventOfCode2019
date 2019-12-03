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
        public void Test1(string path1Csv, string path2Csv, int expected)
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

        private static IReadOnlyCollection<Point> Walk(IEnumerable<string> path)
        {
            var curr = new Point(x: 0, y: 0);
            var points = new List<Point> { curr, };

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

                    points.Add(curr);
                }
            }

            return points.Distinct().ToList();
        }
    }
}
