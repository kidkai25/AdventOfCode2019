using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2019.Common
{
	public static class Helpers
	{
		public async static IAsyncEnumerable<string> ReadLinesAsync(string fileName)
		{
			var path = Path.Combine(Environment.CurrentDirectory, "Data", fileName);

			using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
			using var reader = new StreamReader(stream);

			while (true)
			{
				var line = await reader.ReadLineAsync();

				if (line is null)
				{
					yield break;
				}

				yield return line!;
			}
		}
	}
}
