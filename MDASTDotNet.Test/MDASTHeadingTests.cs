using MDASTDotNet.Conversions;
using MDASTDotNet.LeafBlocks;

namespace MDASTDotNet.Test;

/// <summary>
/// Tests for <see cref="MDASTParser"/> parsing of <see cref="MDASTHeadingNode"/> nodes.
/// </summary>
[TestClass]
public class MDASTHeadingTests
{
	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-62">Thematic Break Example 62</see>
	/// </summary>
	[TestMethod]
	public void BasicDeclarationOfHeading()
	{
		var parser = new MDASTParser();

		var actual = parser.Parse(
			"# foo\n" +
			"## foo\n" +
			"### foo\n" +
			"#### foo\n" +
			"##### foo\n" +
			"###### foo\n"
		);

		var expected = new MDASTRootNode();
		expected.Children.AddRange(new List<MDASTNode>
		{
			new MDASTHeadingNode(1, new MDASTTextNode("foo")),
			new MDASTHeadingNode(2, new MDASTTextNode("foo")),
			new MDASTHeadingNode(3, new MDASTTextNode("foo")),
			new MDASTHeadingNode(4, new MDASTTextNode("foo")),
			new MDASTHeadingNode(5, new MDASTTextNode("foo")),
			new MDASTHeadingNode(6, new MDASTTextNode("foo")),
		});

		Assert.AreEqual(expected, actual);
	}
}
