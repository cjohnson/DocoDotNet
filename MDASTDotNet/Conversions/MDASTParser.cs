﻿using MDASTDotNet.Extensions;
using MDASTDotNet.LeafBlocks;

namespace MDASTDotNet.Conversions
{
    public class MDASTParser
    {
        public MDASTRootNode Parse(string markdown)
        {
            var root = new MDASTRootNode();

            foreach (var line in markdown.Lines())
            {
                var thematicBreak = MDASTThematicBreakNode.TryParse(line);
                if (thematicBreak != null)
                {
                    root.Children.Add(thematicBreak);
                    continue;
                }

                var header = MDASTHeadingNode.TryParse(line);
                if (header != null)
                {
                    root.Children.Add(header);
                    continue;
                }

                // Default to Text Node
                var text = new MDASTTextNode(line);
                root.Children.Add(text);
            }

            return root;
        }
    }
}