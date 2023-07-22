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
	/// Attempts to parse an <see href="https://spec.commonmark.org/0.30/#thematic-breaks">ATX Heading</see> from the given string line 
	/// target according to the <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30 Specification.</see>
	/// </summary>
	/// <param name="target">The target line or section to try to parse.</param>
	/// <returns>A ThematicBreakNode on success, and null on failure.</returns>
	public INode? Parse(string target)
	{
		var match = AsteriskRegex().Match(target);
		if (match.Success)
		{
			return new ThematicBreakNode();
		}

		match = HyphenRegex().Match(target);
		if (match.Success)
		{
			return new ThematicBreakNode();
		}

		match = UnderscoreRegex().Match(target);
		if (match.Success)
		{
			return new ThematicBreakNode();
		}

		return null;
	}
}
