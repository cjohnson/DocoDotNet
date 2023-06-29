using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDASTDotNet.Extensions
{
	internal static class StringExtensions
	{
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

			lines.Add(currentLine);

			return lines;
		}
	}
}
