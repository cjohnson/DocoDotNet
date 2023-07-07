using MDASTDotNet.Conversions;
using MDASTDotNet.LeafBlocks;

namespace MDASTDotNet.Test;

[TestClass]
public class MDASTThematicBreakTests
{
	[TestMethod]
	public void BasicUsageTests()
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
}