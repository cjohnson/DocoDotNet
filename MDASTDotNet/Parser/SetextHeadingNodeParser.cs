using System.Text.RegularExpressions;
using MDASTDotNet.LeafBlocks;

namespace MDASTDotNet.Parser;

/// <summary>
/// Parser of HeadingNode according to the <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30 Specification</see>.
/// <br/><br/>
/// Per the <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see> spec:
/// <br/>
/// A <see href="https://spec.commonmark.org/0.30/#setext-headings">Setext Heading</see> consists of one or more lines of text, not interrupted by a
/// blank line, of which the first line does not have more than 3 spaces of indentation, followed by a setext heading underline. The lines of text
/// must be such that, were they not followed by the setext heading underline, they would be interpreted as a paragraph: they cannot be interpretable
/// as a code fence, ATX heading, block quote, thematic break, list item, or HTML block.
/// </summary>
public partial class SetextHeadingNodeParser : IParser
{
    /// <summary>
    /// Attempts to parse a <see href="https://spec.commonmark.org/0.30/#setext-headings">Setext Heading</see> from the given remaining content lines
    /// according to the <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30 Specification.</see>
    /// </summary>
    /// <param name="contentLines">The remaining lines in the document.</param>
    /// <returns>An MDASTHeadingNode on success, and null on failure.</returns>
    public INode? Parse(List<string> contentLines)
    {
        if (!contentLines.Any())
        {
            return null;
        }

        var headerLevel = 0;
        var headerLines = new List<string>();
        foreach (var contentLine in contentLines)
        {
            if (string.IsNullOrWhiteSpace(contentLine))
            {
                return null;
            }

            if (PrimarySetextHeaderRegex().Match(contentLine).Success)
            {
                if (!headerLines.Any())
                {
                    return null;
                }
                
                headerLevel = 1;
                break;
            }
            
            if (SecondarySetextHeaderRegex().Match(contentLine).Success)
            {
                if (!headerLines.Any())
                {
                    return null;
                }
                
                headerLevel = 2;
                break;
            }

            headerLines.Add(contentLine);
        }

        contentLines.RemoveRange(0, headerLines.Count + 1);
        return new HeadingNode(headerLevel, new TextNode(string.Join('\n', headerLines)));
    }

    [GeneratedRegex("=+")]
    private static partial Regex PrimarySetextHeaderRegex();
    [GeneratedRegex("-+")]
    private static partial Regex SecondarySetextHeaderRegex();
}