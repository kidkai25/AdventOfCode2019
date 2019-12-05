using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace AdventOfCode2019.XUnitTestProject1
{
	public class Day04
	{
		[Theory]
		[InlineData(234_565, false)]
		[InlineData(234_566, true)]
		[InlineData(234_567, false)]
		public void Day04Tests_TestNumber(int number, bool expected)
		{
			var actual = TestNumber(number);

			Assert.Equal(expected, actual);
		}

		private static bool TestNumber(int number)
		{
			if (number < 197_487) return false;
			if (number > 673_251) return false;

			var a = number / 100_000;
			var b = (number / 10_000) % 10;
			var c = (number / 1_000) % 10;
			var d = (number / 100) % 10;
			var e = (number / 10) % 10;
			var f = number % 10;

			if (a > b || b > c || c > d || d > e || e > f) return false;

			return a == b || b == c || c == d || d == e || e == f;
		}

		[Theory]
		[InlineData(1_640)]
		public void Day04Tests_SolvePart1(int expected)
		{
			var count = 0;

			var numbers = GenerateNumbers();

			foreach (var number in numbers)
			{
				var ok = TestNumber(number);

				if (ok)
				{
					count++;
				}
			}

			Assert.Equal(expected, count);
		}

		private static IEnumerable<int> GenerateNumbers()
		{
			for (var a = 0; a <= 9; a++)
			{
				for (var b = a; b <= 9; b++)
				{
					for (var c = b; c <= 9; c++)
					{
						for (var d = c; d <= 9; d++)
						{
							for (var e = d; e <= 9; e++)
							{
								for (var f = e; f <= 9; f++)
								{
									var number = (a * 100000) + (b * 10000) + (c * 1000) + (d * 100) + (e * 10) + f;

									yield return number;
								}
							}
						}
					}
				}
			}
		}

		[Theory]
		[InlineData(223_344, true)]
		[InlineData(234_555, false)]
		[InlineData(222_233, true)]
		public void Day04Tests_TestNumberV2(int number, bool expected)
		{
			var actual = TestNumberV2(number);

			Assert.Equal(expected, actual);
		}

		private static bool TestNumberV2(int number)
		{
			if (number < 197_487) return false;
			if (number > 673_251) return false;

			var a = number / 100_000;
			var b = (number / 10_000) % 10;
			var c = (number / 1_000) % 10;
			var d = (number / 100) % 10;
			var e = (number / 10) % 10;
			var f = number % 10;

			if (a > b || b > c || c > d || d > e || e > f) return false;

			var groups = GetGroups(number);

			return groups.Any(i => i.ToString("D").Length == 2);
		}

		private static readonly Regex _regex = new Regex(@"(.)\1+", RegexOptions.Compiled);

		private static IEnumerable<int> GetGroups(int number)
		{
			var s = number.ToString("D");

			var matches = _regex.Matches(s);

			for (var a = 0; a < matches.Count; a++)
			{
				var value = matches[a].Groups[0].Value;

				yield return int.Parse(value);
			}
		}

		[Theory]
		[InlineData(123_456)]
		[InlineData(123_455, 55)]
		[InlineData(111_122, 1_111, 22)]
		public void Day04Tests_GetGroups(int number, params int[] expected)
		{
			var actual = GetGroups(number).ToList();

			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData(1_126)]
		public void Day04Tests_SolvePart2(int expected)
		{
			var count = 0;

			var numbers = GenerateNumbers();

			foreach (var number in numbers)
			{
				var ok = TestNumberV2(number);

				if (ok)
				{
					count++;
				}
			}

			Assert.Equal(expected, count);
		}
	}
}
