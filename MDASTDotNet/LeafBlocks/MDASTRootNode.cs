using Newtonsoft.Json;

namespace MDASTDotNet.LeafBlocks;

[JsonObject(MemberSerialization.OptIn)]
public class MDASTRootNode : INode
{
	public string Type { get; init; } = "root";

	[JsonProperty("children")]
	public List<INode> Children { get; set; }

	public MDASTRootNode()
	{
		Children = new List<INode>();
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
