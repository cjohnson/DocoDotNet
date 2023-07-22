using MDASTDotNet.Parser;

namespace MDASTDotNet.Test;

[TestClass]
public class BlankLineParsingTests
{
	[TestMethod]
	public void BlankLinesParserRemovesEmptyLines()
	{
		var blankLineParser = new BlankLineParser();
		var contentLines = new List<string>()
		{
			"",
			"# foo",
		};

		blankLineParser.Parse(contentLines);

		Assert.IsTrue(contentLines.SequenceEqual(new List<string>()
		{
			"# foo",
		}));
	}
}
