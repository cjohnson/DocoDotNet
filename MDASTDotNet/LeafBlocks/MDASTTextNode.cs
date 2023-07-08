using MDASTDotNet.Extensions;
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
			Content = ApplyPunctuationBackslashEscapes(content);
		}

		private static string ApplyPunctuationBackslashEscapes(string? content)
		{
			if (content is null)
			{
				return "";
			}

			if (content.Length < 2)
			{
				return content;
			}

			var result = "";
			var escaped = false;
			foreach (var c in content)
			{
				if (c == '\\')
				{
					escaped = true;
					continue;
				}

				if (!escaped)
				{
					result += c;
					continue;
				}

				escaped = false;

				if (c.IsMarkdownPunctuation())
				{
					result += c;
					continue;
				}

				result += '\\';
				result += c;
			}

			return result;
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
