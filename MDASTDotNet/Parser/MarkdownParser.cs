using MDASTDotNet.Extensions;
using MDASTDotNet.LeafBlocks;

namespace MDASTDotNet.Parser;

/// <summary>
/// Parses a Markdown-formatted string according to the <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30 Specification.</see>.
/// </summary>
public class MarkdownParser : IParser
{
    HeadingNodeParser headingNodeParser = new();
    ThematicBreakNodeParser thematicBreakNodeParser = new();

    /// <summary>
    /// Parses a Markdown-formatted string into MDAST according to the given specification.
    /// </summary>
    /// <param name="markdown">The markdown-formatted string</param>
    /// <returns>The MDAST root node</returns>
    public INode Parse(string markdown)
    {
        var root = new RootNode();

        foreach (var line in markdown.Lines())
        {
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            var thematicBreak = thematicBreakNodeParser.Parse(line);
            if (thematicBreak != null)
            {
                root.Children.Add(thematicBreak);
                continue;
            }

            var header = headingNodeParser.Parse(line);
            if (header != null)
            {
                root.Children.Add(header);
                continue;
            }

            // Default to Text Node
            var text = new TextNode(line);
            root.Children.Add(text);
        }

        return root;
    }
}