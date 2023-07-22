using Newtonsoft.Json;

namespace MDASTDotNet.LeafBlocks;

[JsonObject(MemberSerialization.OptIn)]
public class MDASTRootNode : MDASTNode
{
	[JsonProperty("children")]
	public List<MDASTNode> Children { get; set; }

	public MDASTRootNode() : base("root")
	{
		Children = new List<MDASTNode>();
	}

	public override bool Equals(object? obj)
	{
		return obj is MDASTRootNode node &&
			   Type == node.Type &&
			   Children.SequenceEqual(node.Children);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Type, Children);
	}
}
