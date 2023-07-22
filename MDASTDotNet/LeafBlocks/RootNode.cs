using Newtonsoft.Json;

namespace MDASTDotNet.LeafBlocks;

/// <summary>
/// A Root Node is the root container of all of the nodes in the MDAST tree. In a traditional sense, the root node is similar to an
/// entire document. Its children are all of the elements of the document.
/// </summary>
[JsonObject(MemberSerialization.OptIn)]
public class RootNode : INode
{
	public string Type { get; init; } = "root";

	[JsonProperty("children")]
	public List<INode> Children { get; set; }

	public RootNode()
	{
		Children = new List<INode>();
	}

	public override bool Equals(object? obj)
	{
		return obj is RootNode node &&
			   Type == node.Type &&
			   Children.SequenceEqual(node.Children);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Type, Children);
	}
}
