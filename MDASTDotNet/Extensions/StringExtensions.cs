using System.Diagnostics;

namespace MDASTDotNet.Extensions
{
	public static class StringExtensions
	{
		[DebuggerStepThrough]
		public static List<String> Lines(this string target)
		{
			var lines = new List<String>();

			string currentLine = "";
			for (int i = 0; i < target.Length; i++)
			{
				var current = target[i];

				if (current == '\n')
				{
					lines.Add(currentLine);
					currentLine = "";

					continue;
				}

				currentLine += current;
			}

			if (!String.IsNullOrEmpty(currentLine))
			{
				lines.Add(currentLine);
			}

			return lines;
		}
	}
}
