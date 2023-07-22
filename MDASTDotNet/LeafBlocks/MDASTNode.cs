using Newtonsoft.Json;

namespace MDASTDotNet.LeafBlocks;

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
