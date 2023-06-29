using MDASTDotNet.Extensions;
using MDASTDotNet.LeafBlocks;
using Newtonsoft.Json;

namespace MDASTDotNet
{
	[JsonObject(MemberSerialization.OptIn)]
	public abstract class MDASTNode
	{
		[JsonProperty("type")]
		public string Type { get; set; }

		public MDASTNode(string type)
		{
			Type = type;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class MDASTRootNode : MDASTNode
	{
		[JsonProperty("children")]
		public List<MDASTNode> Children { get; set; }

		public MDASTRootNode() : base("root")
		{
			Children = new List<MDASTNode>();
		}
	}

	internal class MDASTParser
	{
		public MDASTRootNode Parse(string markdown)
		{
			var root = new MDASTRootNode();

			foreach (var line in markdown.Lines())
			{
                var thematicBreak = MDASTThematicBreakParser.TryParse(line);
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