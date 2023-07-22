using Newtonsoft.Json;

namespace MDASTDotNet.LeafBlocks;

/// <summary>
/// A Thematic Break node represents a thematic break (traditionally a &lt;br/&gt; tag in HTML)
/// <br/><br/>
/// According to the <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30 Specification</see>:
/// <br/>
/// A line consisting of optionally up to three spaces of indentation, followed by a sequence of three or more matching -, _, or * characters, 
/// each followed optionally by any number of spaces or tabs, forms a <see href="https://spec.commonmark.org/0.30/#thematic-breaks">Thematic Break</see>.
/// </summary>
[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
public class ThematicBreakNode : INode
{
	public string Type { get; init; } = "thematicBreak";

	public override bool Equals(object? obj)
	{
		return obj is ThematicBreakNode;
	}

	public override int GetHashCode()
	{
		return Type.GetHashCode();
	}
}
