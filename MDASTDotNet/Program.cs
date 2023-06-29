using Newtonsoft.Json;

using MDASTDotNet.Extensions;
using MDASTDotNet.LeafBlocks;

namespace MDASTDotNet
{
	internal class MDASTParser
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
			}

            return root;
		}
	}

	internal class Program
	{
		static void Main(string[] args)
		{
			var parser = new MDASTParser();
			var rootNode = parser.Parse("***\n***");

            Console.WriteLine(JsonConvert.SerializeObject(rootNode, Formatting.Indented));
        }
	}
}