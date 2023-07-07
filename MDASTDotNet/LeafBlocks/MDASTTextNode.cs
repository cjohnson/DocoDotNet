using Newtonsoft.Json;

namespace MDASTDotNet.LeafBlocks
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class MDASTTextNode : MDASTNode
	{
		[JsonProperty("content")]
		public string? Content { get; set; }

		public MDASTTextNode(string? content) : base("text")
		{
			Content = content;
		}

		public override bool Equals(object? obj)
		{
			return obj is MDASTTextNode node &&
				   Content == node.Content;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Type, Content);
		}
	}
}
