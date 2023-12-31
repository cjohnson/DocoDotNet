﻿using MDASTDotNet.LeafBlocks;
using MDASTDotNet.Parser;

namespace MDASTDotNet.Test;

/// <summary>
/// Tests for <see cref="MarkdownParser"/> parsing of <see cref="HeadingNode"/>.
/// </summary>
[TestClass]
public class ATXHeadingNodeParsingTests
{
	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-62">Heading Example 62</see>
	/// </summary>
	[TestMethod]
	public void BasicDeclarationOfHeading()
	{
		var parser = new MarkdownParser();

		var actual = parser.Parse(
			"# foo\n" +
			"## foo\n" +
			"### foo\n" +
			"#### foo\n" +
			"##### foo\n" +
			"###### foo\n"
		);

		var expected = new RootNode();
		expected.Children.AddRange(new List<INode>
		{
			new HeadingNode(level: 1, text: new TextNode("foo")),
			new HeadingNode(level: 2, text: new TextNode("foo")),
			new HeadingNode(level: 3, text: new TextNode("foo")),
			new HeadingNode(level: 4, text: new TextNode("foo")),
			new HeadingNode(level: 5, text: new TextNode("foo")),
			new HeadingNode(level: 6, text: new TextNode("foo")),
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
		var parser = new MarkdownParser();

		var actual = parser.Parse(
			"####### foo"
		);

		var expected = new RootNode();
		expected.Children.Add(new TextNode("####### foo"));

		Assert.AreEqual(expected, actual);
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-64">Heading Example 64</see>
	/// </summary>
	[TestMethod]
	public void AtLeastOneSpaceOrTabRequired()
	{
		var parser = new MarkdownParser();

		var actual = parser.Parse(
			"#5 bolt\n" +
			"\n" +
			"#hashtag"
		);

		var expected = new RootNode();
		expected.Children.AddRange(new List<INode>
		{
			new TextNode("#5 bolt"),
			new TextNode("#hashtag"),
		});
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-65">Heading Example 65</see>
	/// </summary>
	[TestMethod]
	public void EscapedHeadingsAreIgnored()
	{
		var parser = new MarkdownParser();

		var actual = parser.Parse(
			"\\## foo"
		);

		var expected = new RootNode();
		expected.Children.Add(new TextNode("## foo"));

		Assert.AreEqual(expected, actual);
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-66">Heading Example 66</see>
	/// </summary>
	[TestMethod]
	public void ContentsAreParsedAsInlines()
	{
		var parser = new MarkdownParser();

		var actual = parser.Parse(
			"# foo *bar* \\*baz\\*"
		);

		// This will fail eventually when MDASTEmphasisNode is added. When this fails due to this reason,
		// simply change the output expectation to use MDASTEmphasisNode (or the theoretical equivalent).
		var expected = new RootNode();
		expected.Children.Add(new HeadingNode(level: 1, text: new TextNode("foo *bar* *baz*")));

		Assert.AreEqual(expected, actual);
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-67">Heading Example 67</see>
	/// </summary>
	[TestMethod]
	public void LeadingAndTrailingSpacesAreIgnoredInInlineContent()
	{
		var parser = new MarkdownParser();

		var actual = parser.Parse(
			"#                  foo                     "
		);

		var expected = new RootNode();
		expected.Children.Add(new HeadingNode(level: 1, text: new TextNode("foo")));

		Assert.AreEqual(expected, actual);
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-68">Heading Example 68</see>
	/// </summary>
	[TestMethod]
	public void UpToThreeSpacesOfIndentationAreAllowed()
	{
		var parser = new MarkdownParser();

		var actual = parser.Parse(
			" ### foo\n" +
			"  ## foo\n" +
			"   # foo\n"
		);

		var expected = new RootNode();
		expected.Children.AddRange(new List<INode>
		{
			new HeadingNode(level: 3, text: new TextNode("foo")),
			new HeadingNode(level: 2, text: new TextNode("foo")),
			new HeadingNode(level: 1, text: new TextNode("foo")),
		});

		Assert.AreEqual(expected, actual);
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-69">Heading Example 69</see>
	/// and
	/// <see href="https://spec.commonmark.org/0.30/#example-70">Heading Example 70</see>
	/// </summary>
	[TestMethod]
	public void FourSpacesOfIndentationIsTooMany()
	{
		var parser = new MarkdownParser();

		var actual = parser.Parse(
			"    # foo\n" +
			"foo\n" +
			"    # bar"
		);

		var expected = new RootNode();
		expected.Children.AddRange(new List<INode>
		{
			new TextNode("    # foo"),
			new TextNode("foo"),
			new TextNode("    # bar"),
		});

		Assert.AreEqual(expected, actual);
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-71">Heading Example 71</see>
	/// </summary>
	[TestMethod]
	public void ClosingSequencesAreAllowed()
	{
		var parser = new MarkdownParser();

		var actual = parser.Parse(
			"## foo ##\n" +
			"  ###   bar    ###\n"
		);

		var expected = new RootNode();
		expected.Children.AddRange(new List<INode>
		{
			new HeadingNode(level: 2, text: new TextNode("foo")),
			new HeadingNode(level: 3, text: new TextNode("bar")),
		});

		Assert.AreEqual(expected, actual);
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-72">Heading Example 72</see>
	/// </summary>
	[TestMethod]
	public void ClosingSequencesDoNotNeedToMatchHeaderLevel()
	{
		var parser = new MarkdownParser();

		var actual = parser.Parse(
			"# foo ##################################\n" +
			"##### foo ##\n"
		);

		var expected = new RootNode();
		expected.Children.AddRange(new List<INode>
		{
			new HeadingNode(level: 1, text: new TextNode("foo")),
			new HeadingNode(level: 5, text: new TextNode("foo")),
		});

		Assert.AreEqual(expected, actual);
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-73">Heading Example 73</see>
	/// </summary>
	[TestMethod]
	public void SpacesOrTabsAreAllowedAfterTheClosingSequence()
	{
		var parser = new MarkdownParser();

		var actual = parser.Parse(
			"### foo ###     \n"
		);

		var expected = new RootNode();
		expected.Children.AddRange(new List<INode>
		{
			new HeadingNode(level: 3, text: new TextNode("foo")),
		});

		Assert.AreEqual(expected, actual);
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-74">Heading Example 74</see>
	/// </summary>
	[TestMethod]
	public void ClosingSequencesWithAnythingButTabsOrSpacesFollowingItBecomeContentsOfTheHeading()
	{
		var parser = new MarkdownParser();

		var actual = parser.Parse(
			"### foo ### b\n"
		);

		var expected = new RootNode();
		expected.Children.AddRange(new List<INode>
		{
			new HeadingNode(level: 3, text: new TextNode("foo ### b")),
		});

		Assert.AreEqual(expected, actual);
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-75">Heading Example 75</see>
	/// </summary>
	[TestMethod]
	public void ClosingSequencesMustBePreceededByASpaceOrTab()
	{
		var parser = new MarkdownParser();

		var actual = parser.Parse(
			"# foo#\n"
		);

		var expected = new RootNode();
		expected.Children.AddRange(new List<INode>
		{
			new HeadingNode(level: 1, text: new TextNode("foo#")),
		});

		Assert.AreEqual(expected, actual);
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-76">Heading Example 76</see>
	/// </summary>
	[TestMethod]
	public void BackslashEscapedHeadingCharactersDoNotCountAsPartOfTheClosingSequence()
	{
		var parser = new MarkdownParser();

		var actual = parser.Parse(
			"### foo \\###\n" +
			"## foo #\\##\n" +
			"# foo \\#\n"
		);

		var expected = new RootNode();
		expected.Children.AddRange(new List<INode>
		{
			new HeadingNode(level: 3, text: new TextNode("foo ###")),
			new HeadingNode(level: 2, text: new TextNode("foo ###")),
			new HeadingNode(level: 1, text: new TextNode("foo #")),
		});

		Assert.AreEqual(expected, actual);
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-77">Heading Example 77</see>
	/// </summary>
	[TestMethod]
	public void HeadingsDoNotNeedBlankLinesSurroundingThem()
	{
		var parser = new MarkdownParser();

		var actual = parser.Parse(
			"****\n" +
			"## foo \n" +
			"****"
		);

		var expected = new RootNode();
		expected.Children.AddRange(new List<INode>
		{
			new ThematicBreakNode(),
			new HeadingNode(level: 2, text: new TextNode("foo")),
			new ThematicBreakNode(),
		});

		Assert.AreEqual(expected, actual);
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-78">Heading Example 78</see>
	/// </summary>
	[TestMethod]
	public void HeadingsCanInterruptParagraphs()
	{
		var parser = new MarkdownParser();

		var actual = parser.Parse(
			"Foo bar\n" +
			"# baz\n" +
			"Bar foo"
		);

		var expected = new RootNode();
		expected.Children.AddRange(new List<INode>
		{
			new TextNode("Foo bar"),
			new HeadingNode(level: 1, text: new TextNode("baz")),
			new TextNode("Bar foo"),
		});

		Assert.AreEqual(expected, actual);
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-79">Heading Example 79</see>
	/// </summary>
	[TestMethod]
	public void HeadingsCanBeEmpty()
	{
		var parser = new MarkdownParser();

		var actual = parser.Parse(
			"##\n" +
			"#\n" +
			"### ###"
		);

		var expected = new RootNode();
		expected.Children.AddRange(new List<INode>
		{
			new HeadingNode(level: 2, text: new TextNode("")),
			new HeadingNode(level: 1, text: new TextNode("")),
			new HeadingNode(level: 3, text: new TextNode("")),
		});

		Assert.AreEqual(expected, actual);
	}
}
