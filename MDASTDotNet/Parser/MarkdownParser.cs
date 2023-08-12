using MDASTDotNet.Extensions;
using MDASTDotNet.LeafBlocks;

namespace MDASTDotNet.Parser;

/// <summary>
/// Parses a Markdown-formatted string according to the <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30 Specification.</see>.
/// </summary>
public class MarkdownParser
{
	private List<IParser> Parsers { get; }

    public MarkdownParser()
    {
		Parsers = Presets.CommonMark3_0;
    }

    public MarkdownParser(List<IParser> parsers)
    {
        Parsers = parsers;
    }

    /// <summary>
    /// Parses a Markdown-formatted string into MDAST according to the given specification.
    /// </summary>
    /// <param name="markdown">The markdown-formatted string</param>
    /// <returns>The MDAST root node</returns>
    public INode Parse(string markdown)
    {
        var root = new RootNode();

        var lines = markdown.Lines();

        while (lines.Any())
        {
	        foreach (var node in Parsers.Select(parser => parser.Parse(lines)).Where(node => node != null))
	        {
		        if (node != null) root.Children.Add(node);
		        break;
	        }
        }

		return root;
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public static class Presets
    {
        public static List<IParser> CommonMark3_0 => new()
        {
            new BlankLineParser(),
            new ThematicBreakNodeParser(),
            new SetextHeadingNodeParser(),
            new AtxHeadingNodeParser(),
            new TextNodeParser(),
        };
    }
}