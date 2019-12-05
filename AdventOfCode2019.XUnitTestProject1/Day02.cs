using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2019.XUnitTestProject1
{
	public class Day02
	{
		[Theory]
		[InlineData(
			"1,9,10,3,2,3,11,0,99,30,40,50",
			"3500,9,10,70,2,3,11,0,99,30,40,50")]
		[InlineData("1,0,0,0,99", "2,0,0,0,99")]
		[InlineData("2,3,0,3,99", "2,3,0,6,99")]
		[InlineData("2,4,4,5,99,0", "2,4,4,5,99,9801")]
		[InlineData("1,1,1,4,99,5,6,0,99", "30,1,1,4,2,5,6,0,99")]
		public void PerformIntcodeTests(string ints, string expected)
		{
			var actual = string.Join(',', PerformIntcode(ints));

			Assert.Equal(expected, actual);
		}

		private static IList<int> PerformIntcode(string ints)
			=> PerformIntcode(new List<int>(ints.Split(',').Select(int.Parse)));

		private static IList<int> PerformIntcode(IList<int> ints)
		{
			for (var a = 0; a < ints.Count && ints[a] != 99; a += 4)
			{
				int x, y;

				switch (ints[a])
				{
					case 1:
						x = ints[ints[a + 1]];
						y = ints[ints[a + 2]];
						ints[ints[a + 3]] = x + y;
						break;
					case 2:
						x = ints[ints[a + 1]];
						y = ints[ints[a + 2]];
						ints[ints[a + 3]] = x * y;
						break;
				}
			}

			return ints;
		}

		[Theory]
		[InlineData("day02.txt", 12_490_719)]
		public async Task SolvePart1(string fileName, int expected)
		{
			var line = await Helpers.ReadLineAsync(fileName);

			var ints = new List<int>(line.Split(',').Select(int.Parse))
			{
				//fix
				[1] = 12,
				[2] = 2,
			};

			var after = PerformIntcode(ints);

			Assert.Equal(expected, after[0]);
		}

		[Theory]
		[InlineData("day02.txt", 20, 3)]
		public async Task SolvePart2(string fileName, int expectedNoun, int expectedVerb)
		{
			var line = await Helpers.ReadLineAsync(fileName);

			for (var noun = 0; noun <= 99; noun++)
			{
				for (var verb = 0; verb <= 99; verb++)
				{
					var ints = new List<int>(line.Split(',').Select(int.Parse))
					{
						//fix
						[1] = noun,
						[2] = verb,
					};

					var after = PerformIntcode(ints);

					if (after[0] == 19_690_720)
					{
						Assert.Equal(expectedNoun, noun);
						Assert.Equal(expectedVerb, verb);
						return;
					}
				}
			}

			Assert.True(false);
		}
	}
}
