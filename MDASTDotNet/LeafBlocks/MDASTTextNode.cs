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
	}
}
