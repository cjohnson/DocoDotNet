using Newtonsoft.Json;

namespace MDASTDotNet.LeafBlocks;

/// <summary>
/// An MDAST Heading Node is a data model representation of the <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see> leaf node
/// <see href="https://spec.commonmark.org/0.30/#atx-headings">ATX Heading</see>.
/// <br/><br/>
/// Per the <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see> spec:
/// <br/>
/// An <see href="https://spec.commonmark.org/0.30/#atx-headings">ATX Heading</see> consists of a string of characters, parsed as inline content, 
/// between an opening sequence of 1–6 unescaped # characters and an optional closing sequence of any number of unescaped # characters. The 
/// opening sequence of # characters must be followed by spaces or tabs, or by the end of line. The optional closing sequence of #s must be 
/// preceded by spaces or tabs and may be followed by spaces or tabs only. The opening # character may be preceded by up to three spaces of 
/// indentation. The raw contents of the heading are stripped of leading and trailing space or tabs before being parsed as inline content. 
/// The heading level is equal to the number of # characters in the opening sequence.
/// </summary>
[JsonObject(MemberSerialization.OptIn)]
public partial class HeadingNode : MDASTNode
{
	/// <summary>
	/// The level of the header. Traditionally, this corresponds to the HTML header level.
	/// <br/>
	/// <list type="bullet">
	/// <item>
	///		The markdown "<c>#</c>" (Level = 1) would translate to an "&lt;h1&gt;" tag in HTML.
	/// </item>
	/// <item>
	///		The markdown "<c>##</c>" (Level = 2) would translate to an "&lt;h2&gt;" tag in HTML.
	/// </item>
	/// </list>
	/// And so on.
	/// <br/>
	/// In terms of CommonMark itself, the Level property simply corresponds to the number of hashtags (#) used prior to the Text content.
	/// </summary>
	[JsonProperty("level")]
	public int Level { get; set; }

	/// <summary>
	/// The text content of the header. This is the text that goes inside the HTML tag, in a traditional markdown use case.
	/// <br/>
	/// The markdown "<c># My Content</c>" would translate to "<c>&lt;h1&gt;My Content&lt;/h1&gt;</c>" in HTML.
	/// <br/>
	/// In this example, Text = "My Content".
	/// </summary>
	[JsonProperty("text")]
	public MDASTTextNode? Text { get; set; }

	/// <summary>
	/// Construct a new MDASTHeadingNode.
	/// <br/>
	/// The heading level must be greater than 0, and less than 7. (1-6). Throws ArgumentException if this rule is violated.
	/// </summary>
	/// <param name="level">The heading level. Must be greater than 0, and less than 7. (1-6). Throws ArgumentException if violated.</param>
	/// <param name="text">The text content of the heading.</param>
	/// <exception cref="ArgumentException"></exception>
	[JsonConstructor]
	public HeadingNode(int level, MDASTTextNode? text) : base("heading")
	{
		if (level < 0)
		{
			throw new ArgumentException("Heading Level cannot be less than 0.");
		}

		if (level > 6)
		{
			throw new ArgumentException("Heading level cannot be greater than 6.");
		}

		Level = level;
		Text = text;
	}

	public override bool Equals(object? obj)
	{
		return obj is HeadingNode node &&
			   Level == node.Level &&
			   EqualityComparer<MDASTTextNode?>.Default.Equals(Text, node.Text);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Type, Level, Text);
	}
}
