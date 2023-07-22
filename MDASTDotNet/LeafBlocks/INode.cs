using Newtonsoft.Json;

namespace MDASTDotNet.LeafBlocks;

/// <summary>
/// The base node type of MDAST. Each node type implements this interface,
/// having a type, which is used to help serialize/deserialize from JSON.
/// </summary>
[JsonObject(MemberSerialization.OptIn)]
public interface INode
{
	/// <summary>
	/// The type name of the node. Used to help serialize/deserialize from JSON.
	/// </summary>
	[JsonProperty("type")]
	public string Type { get; init; }
}
