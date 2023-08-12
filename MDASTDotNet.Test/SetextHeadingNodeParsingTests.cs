using MDASTDotNet.LeafBlocks;
using MDASTDotNet.Parser;

namespace MDASTDotNet.Test;

/// <summary>
/// Tests for <see cref="MarkdownParser"/> parsing of <see cref="HeadingNode"/>.
/// </summary>
[TestClass]
public class SetextHeadingNodeParsingTests
{
    /// <summary>
    /// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
    /// <see href="https://spec.commonmark.org/0.30/#example-80">Heading Example 80</see>
    /// </summary>
    [TestMethod]
    public void BasicDeclarationOfHeading()
    {
        var parser = new MarkdownParser();

        var actual = parser.Parse(
            "Foo *bar*\n" +
            "=========\n" +
            "\n" +
            "Foo *bar*\n" +
            "---------\n"
        );
        
        // Note: this test will break once Emphasis is included in the parsing chain.
        var expected = new RootNode();
        expected.Children.AddRange(new List<INode>()
        {
            new HeadingNode(level: 1, new TextNode("Foo *bar*")),
            new HeadingNode(level: 2, new TextNode("Foo *bar*")),
        });
        
        Assert.AreEqual(expected, actual);
    }
    
    /// <summary>
    /// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
    /// <see href="https://spec.commonmark.org/0.30/#example-81">Heading Example 81</see>
    /// </summary>
    [TestMethod]
    public void HeaderContentMaySpanMoreThanOneLine()
    {
        var parser = new MarkdownParser();

        var actual = parser.Parse(
            "Foo *bar\n" +
            "baz*\n" +
            "=========\n"
        );

        var expected = new RootNode();
        expected.Children.Add(new HeadingNode(level: 1, new TextNode("Foo *bar\nbaz*")));
        
        Assert.AreEqual(expected, actual);
    }
}