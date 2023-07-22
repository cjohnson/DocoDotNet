using MDASTDotNet.Extensions;
using Newtonsoft.Json;

namespace MDASTDotNet.LeafBlocks;

/// <summary>
/// A Text node is the default case in an MDAST parsing. If the target is not recognized by any other parser, it usually ends up
/// as a text element.
/// </summary>
[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
public class TextNode : INode
{
	public string Type { get; init; } = "text";

	/// <summary>
	/// The actual string text content.
	/// </summary>
	[JsonProperty("content")]
	public string? Content { get; set; }

	public TextNode(string? content)
	{
		Content = ApplyPunctuationBackslashEscapes(content);
	}

	/// <summary>
	/// Looks for backslash escapes, and properly escapes any markdown punctuation, if applicable.
	/// </summary>
	/// <param name="content">The content</param>
	/// <returns></returns>
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
		return obj is TextNode node &&
			   Content == node.Content;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Type, Content);
	}
}
