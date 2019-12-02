using AdventOfCode2019.Common;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2019.Day01
{
	public class Part1Tests
	{
		[Theory]
		[InlineData(12, 2)]
		[InlineData(14, 2)]
		[InlineData(1_969, 654)]
		[InlineData(100_756, 33_583)]
		public void CalculateFuelTests(int mass, int expected)
		{
			var actual = CalculateFuel(mass);

			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData("input.txt")]
		public async Task ReadLinesAsyncTests(string fileName)
		{
			var count = 0;
			var lines = Helpers.ReadLinesAsync(fileName);

			await foreach (var line in lines)
			{
				count++;

				Assert.NotNull(line);
				Assert.NotEmpty(line);
			}

			Assert.InRange(count, 1, int.MaxValue);
		}

		[Theory]
		[InlineData("input.txt", 3_317_100)]
		public async Task CalculateTotalFuelTests(string fileName, int expected)
		{
			int totalFuel = 0;
			var lines = Helpers.ReadLinesAsync(fileName);

			await foreach (var line in lines)
			{
				var mass = int.Parse(line);
				var fuel = CalculateFuel(mass);
				totalFuel += fuel;
			}

			Assert.Equal(expected, totalFuel);
		}

		[Theory]
		[InlineData(14, 2)]
		[InlineData(1_969, 966)]
		[InlineData(100_756, 50_346)]
		public void CalculateFuelRecursivelyTests(int mass, int expected)
		{
			var actual = CalculateFuelRecursively(mass);

			Assert.Equal(expected, actual);
		}

		private static int CalculateFuel(int mass) => (mass / 3) - 2;

		private static int CalculateFuelRecursively(int mass)
		{
			var fuel = CalculateFuel(mass);
			var totalFuel = fuel;

			while (true)
			{
				fuel = CalculateFuel(fuel);

				if (fuel <= 0)
				{
					break;
				}

				totalFuel += fuel;
			}

			return totalFuel;
		}

		[Theory]
		[InlineData("input.txt", 4_972_784)]
		public async Task CalculateTotalTotalFuelTests(string fileName, int expected)
		{
			var total = 0;
			var lines = Helpers.ReadLinesAsync(fileName);

			await foreach (var line in lines)
			{
				var mass = int.Parse(line);
				var fuel = CalculateFuelRecursively(mass);
				total += fuel;
			}

			Assert.Equal(expected, total);
		}
	}
}
