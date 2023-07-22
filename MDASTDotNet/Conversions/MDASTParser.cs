using MDASTDotNet.Extensions;
using MDASTDotNet.LeafBlocks;
using MDASTDotNet.Parser;

namespace MDASTDotNet.Conversions
{
    public class MDASTParser
    {
        HeadingNodeParser headingNodeParser = new();

        public MDASTRootNode Parse(string markdown)
        {
            var root = new MDASTRootNode();

            foreach (var line in markdown.Lines())
            {
                if (String.IsNullOrEmpty(line))
                {
                    continue;
                }

                var thematicBreak = MDASTThematicBreakNode.TryParse(line);
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
                var text = new MDASTTextNode(line);
                root.Children.Add(text);
            }

            return root;
        }
    }
}