using MDASTDotNet.Conversions;
using MDASTDotNet.LeafBlocks;

namespace MDASTDotNet.Test;

[TestClass]
public class MDASTThematicBreakTests
{
	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-43">Thematic Break Example 43</see>
	/// </summary>
	[TestMethod]
	public void BasicDeclarationOfThematicBreak()
	{
		var parser = new MDASTParser();

		var actual = parser.Parse(
			"***\n" +
			"---\n" +
			"___"
		);

		var expected = new MDASTRootNode();
		expected.Children.AddRange(new List<MDASTNode>
		{
			new MDASTThematicBreakNode(),
			new MDASTThematicBreakNode(),
			new MDASTThematicBreakNode()
		});
		
		Assert.AreEqual(expected, actual);
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-44">Thematic Break Example 44</see>
	/// and
	/// <see href="https://spec.commonmark.org/0.30/#example-45">Thematic Break Example 45</see>
	/// </summary>
	[DataRow("+++")]
	[DataRow("===")]
	[TestMethod]
	public void WrongCharacterDeclarationOfThematicBreak(string markdownInput)
	{
		var parser = new MDASTParser();

		var actual = parser.Parse(markdownInput);

		var expected = new MDASTRootNode();
		expected.Children.Add(new MDASTTextNode(markdownInput));

		Assert.AreEqual(expected, actual);
	}
}