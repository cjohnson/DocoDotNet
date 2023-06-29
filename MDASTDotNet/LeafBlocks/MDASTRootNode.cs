using Newtonsoft.Json;

namespace MDASTDotNet.LeafBlocks
{
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
}
