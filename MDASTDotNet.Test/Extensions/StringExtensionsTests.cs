using MDASTDotNet.Extensions;

namespace MDASTDotNet.Test.Extensions;

[TestClass]
public class StringExtensionsTests
{
	/// <summary>
	/// A newline character at the end of the input string to the string.Lines() extension should not append an empty, 
	/// non-null string to the end of the list.
	/// </summary>
	[TestMethod]
	public void NewLineAtEndOfFileIsNotIncluded()
	{
		var testCase = "Foo\nBar\n";

		var actual = testCase.Lines();

		var expected = new List<string>
		{
			"Foo",
			"Bar",
		};

		CollectionAssert.AreEqual(expected, actual);
	}
}
