﻿using MDASTDotNet.Extensions;
using MDASTDotNet.LeafBlocks;

namespace MDASTDotNet.Parser;

/// <summary>
/// Parses a Markdown-formatted string according to the <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30 Specification.</see>.
/// </summary>
public class MarkdownParser : IParser
{
	public List<IParser> Parsers { get; set; }

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

        foreach (var line in markdown.Lines())
        {
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            INode? node = null;
            foreach (var parser in Parsers)
            {
                node = parser.Parse(line);
                if (node != null)
                {
                    root.Children.Add(node);
                    break;
                }
            }

            if (node is not null)
            {
                continue;
            }

			// Default to Text Node
			var text = new TextNode(line);
			root.Children.Add(text);
		}

        return root;
    }

    public static class Presets
    {
        public static List<IParser> CommonMark3_0 => new()
        {
            new ThematicBreakNodeParser(),
            new ATXHeadingNodeParser(),
        };
    }
}