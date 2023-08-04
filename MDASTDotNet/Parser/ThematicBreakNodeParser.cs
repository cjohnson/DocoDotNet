using MDASTDotNet.LeafBlocks;
using System.Text.RegularExpressions;

namespace MDASTDotNet.Parser;

/// <summary>
/// Parser of ThematicBreakNode according to the <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30 Specification.</see>
/// </summary>
internal partial class ThematicBreakNodeParser : IParser
{
	[GeneratedRegex(@"^ {0,3}(?:- *){3,}$")]
	private static partial Regex HyphenRegex();

	[GeneratedRegex(@"^ {0,3}(?:\* *){3,}$")]
	private static partial Regex AsteriskRegex();

	[GeneratedRegex(@"^ {0,3}(?:_ *){3,}$")]
	private static partial Regex UnderscoreRegex();

	/// <summary>
	/// Attempts to parse an <see href="https://spec.commonmark.org/0.30/#thematic-breaks">ATX Heading</see> from the given remaining content lines
	/// according to the <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30 Specification.</see>
	/// </summary>
	/// <param name="contentLines">The remaining content lines in the document.</param>
	/// <returns>A ThematicBreakNode on success, and null on failure.</returns>
	public INode? Parse(List<string> contentLines)
	{
		if (!contentLines.Any())
		{
			return null;
		}

		var match = AsteriskRegex().Match(contentLines.First());
		if (match.Success)
		{
			contentLines.RemoveAt(0);
			return new ThematicBreakNode();
		}

		match = HyphenRegex().Match(contentLines.First());
		if (match.Success)
		{
			contentLines.RemoveAt(0);
			return new ThematicBreakNode();
		}

		match = UnderscoreRegex().Match(contentLines.First());
		if (!match.Success) return null;
		contentLines.RemoveAt(0);
		return new ThematicBreakNode();
	}
}
