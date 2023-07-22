using MDASTDotNet.LeafBlocks;

namespace MDASTDotNet.Test;

[TestClass]
public class MDASTTextNodeTests
{
	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-12">Backslash Escapes Example 12</see>
	/// </summary>
	[TestMethod]
	public void PunctuationCharactersAreBackslashEscaped()
	{
		var actual = new TextNode(
			"\\!" +
			"\\\"" +
			"\\;" +
			"\\#" +
			"\\$" +
			"\\%" +
			"\\&" +
			"\\'" +
			"\\(" +
			"\\)" +
			"\\*" +
			"\\+" +
			"\\," +
			"\\-" +
			"\\." +
			"\\/" +
			"\\:" +
			"\\;" +
			"\\<" +
			"\\=" +
			"\\>" +
			"\\?" +
			"\\@" +
			"\\[" +
			"\\\\" +
			"\\]" +
			"\\^" +
			"\\_" +
			"\\`" +
			"\\{" +
			"\\|" +
			"\\}" +
			"\\~"
		);

		var expected = new TextNode(
			"!" +
			"\"" +
			";" +
			"#" +
			"$" +
			"%" +
			"&" +
			"'" +
			"(" +
			")" +
			"*" +
			"+" +
			"," +
			"-" +
			"." +
			"/" +
			":" +
			";" +
			"<" +
			"=" +
			">" +
			"?" +
			"@" +
			"[" +
			"\\" +
			"]" +
			"^" +
			"_" +
			"`" +
			"{" +
			"|" +
			"}" +
			"~"
		);

		Assert.AreEqual(expected, actual);
	}
}
