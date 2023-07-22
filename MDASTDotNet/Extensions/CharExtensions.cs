using System.Diagnostics;

namespace MDASTDotNet.Extensions;

internal static class CharExtensions
{
	[DebuggerStepThrough]
	internal static bool MatchesAny(this char c, params char[] tests)
	{
		foreach (var test in tests)
		{
			if (c == test)
			{
				return true;
			}
		}

		return false;
	}

	[DebuggerStepThrough]
	internal static bool IsMarkdownPunctuation(this char c) =>
		c.MatchesAny('!', '\"', ';', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/', ':', ';', '<', '=', '>', '?', '@', '[', '\\', ']', '^', '_', '`', '{', '|', '}', '~');
}
