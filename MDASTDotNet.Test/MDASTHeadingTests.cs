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
	/// <see href="https://spec.commonmark.org/0.30/#example-62">Heading Example 62</see>
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

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-63">Heading Example 63</see>
	/// </summary>
	[TestMethod]
	public void MoreThanSixHashtagsIsNotAHeading()
	{
		var parser = new MDASTParser();

		var actual = parser.Parse(
			"####### foo"	
		);

		var expected = new MDASTRootNode();
		expected.Children.Add(new MDASTTextNode("####### foo"));

		Assert.AreEqual(expected, actual);
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-64">Heading Example 64</see>
	/// </summary>
	[TestMethod]
	public void AtLeastOneSpaceOrTabRequired()
	{
		var parser = new MDASTParser();

		var actual = parser.Parse(
			"#5 bolt\n" +
			"\n" +
			"#hashtag"
		);

		var expected = new MDASTRootNode();
		expected.Children.AddRange(new List<MDASTNode>
		{
			new MDASTTextNode("#5 bolt"),
			new MDASTTextNode("#hashtag"),
		});
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-65">Heading Example 65</see>
	/// </summary>
	[TestMethod]
	public void EscapedHeadingsAreIgnored()
	{
		var parser = new MDASTParser();

		var actual = parser.Parse(
			"\\## foo"
		);

		var expected = new MDASTRootNode();
		expected.Children.Add(new MDASTTextNode("## foo"));

		Assert.AreEqual(expected, actual);
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-66">Heading Example 66</see>
	/// </summary>
	[TestMethod]
	public void ContentsAreParsedAsInlines()
	{
		var parser = new MDASTParser();

		var actual = parser.Parse(
			"# foo *bar* \\*baz\\*"
		);

		// This will fail eventually when MDASTEmphasisNode is added. When this fails due to this reason,
		// simply change the output expectation to use MDASTEmphasisNode (or the theoretical equivalent).
		var expected = new MDASTRootNode();
		expected.Children.Add(new MDASTHeadingNode(level: 1, text: new MDASTTextNode("foo *bar* *baz*")));

		Assert.AreEqual(expected, actual);
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-67">Heading Example 67</see>
	/// </summary>
	[TestMethod]
	public void LeadingAndTrailingSpacesAreIgnoredInInlineContent()
	{
		var parser = new MDASTParser();

		var actual = parser.Parse(
			"#                  foo                     "
		);

		// This will fail eventually when MDASTEmphasisNode is added. When this fails due to this reason,
		// simply change the output expectation to use MDASTEmphasisNode (or the theoretical equivalent).
		var expected = new MDASTRootNode();
		expected.Children.Add(new MDASTHeadingNode(level: 1, text: new MDASTTextNode("foo")));

		Assert.AreEqual(expected, actual);
	}
}
