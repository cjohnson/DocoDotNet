using MDASTDotNet.LeafBlocks;
using System.Text.RegularExpressions;

namespace MDASTDotNet.Parser;

/// <summary>
/// Parser of HeadingNode according to the <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30 Specification</see>.
/// <br/><br/>
/// Per the <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see> spec:
/// <br/>
/// An <see href="https://spec.commonmark.org/0.30/#atx-headings">ATX Heading</see> consists of a string of characters, parsed as inline content, 
/// between an opening sequence of 1–6 unescaped # characters and an optional closing sequence of any number of unescaped # characters. The 
/// opening sequence of # characters must be followed by spaces or tabs, or by the end of line. The optional closing sequence of #s must be 
/// preceded by spaces or tabs and may be followed by spaces or tabs only. The opening # character may be preceded by up to three spaces of 
/// indentation. The raw contents of the heading are stripped of leading and trailing space or tabs before being parsed as inline content. 
/// The heading level is equal to the number of # characters in the opening sequence.
/// </summary>
internal partial class ATXHeadingNodeParser : IParser
{
	/// <summary>
	/// Regex that matches an <see href="https://spec.commonmark.org/0.30/#atx-headings">ATX Heading</see> according to the
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30 Specification.</see>
	/// </summary>
	[GeneratedRegex(@"^(?:(?: {0,3}|\t{0,3})(#{1,6})(?: +|\t+|$))(?:([^#\n].*?)(?: +|\t+|$))?(?:#*)?(?: *|\t*)$", RegexOptions.Multiline)]
	private static partial Regex ATXHeadingRegex();

	/// <summary>
	/// Attempts to parse an <see href="https://spec.commonmark.org/0.30/#atx-headings">ATX Heading</see> from the given string line 
	/// target according to the <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30 Specification.</see>
	/// </summary>
	/// <param name="target">The target line or section to try to parse.</param>
	/// <returns>An MDASTHeadingNode on success, and null on failure.</returns>
	public INode? Parse(string target)
	{
		var headingRegex = ATXHeadingRegex();

		var match = headingRegex.Match(target);
		if (!match.Success)
		{
			return null;
		}

		// The first capturing group (2nd group) in the ATX Heading regex is the header hashtags substring,
		// whose length corresponds to the header level. (For example, '##' -> Level = 2)
		var level = match.Groups[1].Value.Length;
		var text = new TextNode(match.Groups[2].Value);

		return new HeadingNode(level, text);
	}
}
