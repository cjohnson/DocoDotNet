﻿using MDASTDotNet.LeafBlocks;
using System.Text.RegularExpressions;

namespace MDASTDotNet.Parser;

internal partial class HeadingNodeParser
{
	/// <summary>
	/// Regex that matches an <see href="https://spec.commonmark.org/0.30/#atx-headings">ATX Heading</see> according to the
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30 Specification.</see>
	/// </summary>
	[GeneratedRegex(@"^(?:(?: {0,3}|\t{0,3})(#{1,6})(?: +|\t+|$))(?:([^#\n].*?)(?: +|\t+|$))?(?:#*)?(?: *|\t*)$", RegexOptions.Multiline)]
	internal static partial Regex HeadingRegex();

	/// <summary>
	/// Attempts to parse an <see href="https://spec.commonmark.org/0.30/#atx-headings">ATX Heading</see> from the given string line 
	/// target according to the <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30 Specification.</see>
	/// </summary>
	/// <param name="target">The target line or section to try to parse.</param>
	/// <returns>An MDASTHeadingNode on success, and null on failure.</returns>
	internal HeadingNode? Parse(string target)
	{
		var headingRegex = HeadingRegex();

		var match = headingRegex.Match(target);
		if (!match.Success)
		{
			return null;
		}

		var level = match.Groups[1].Value.Length;
		var text = new MDASTTextNode(match.Groups[2].Value);

		return new HeadingNode(level, text);
	}
}