using MDASTDotNet.Conversions;
using MDASTDotNet.LeafBlocks;

namespace MDASTDotNet.Test;

/// <summary>
/// Tests for <see cref="MDASTParser"/> parsing of <see cref="MDASTThematicBreakNode"/> nodes.
/// </summary>
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
			new MDASTThematicBreakNode(),
		});
		
		Assert.AreEqual(expected, actual);
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-44">Thematic Break Example 44</see>
	/// and
	/// <see href="https://spec.commonmark.org/0.30/#example-45">Thematic Break Example 45</see>
	/// </summary>
	[TestMethod]
	public void WrongCharactersDeclarationOfThematicBreak()
	{
		var parser = new MDASTParser();

		var actual = parser.Parse(
			"+++\n" +
			"==="
		);

		// This will fail eventually when MDASTTextNode parsing is fixed. The parser will read the text as one
		// all-inclusive MDASTTextNode, with text "+++\n===".
		var expected = new MDASTRootNode();
		expected.Children.AddRange(new List<MDASTNode>
		{
			new MDASTTextNode("+++"),
			new MDASTTextNode("==="),
		});

		Assert.AreEqual(expected, actual);
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-46">Thematic Break Example 46</see>
	/// </summary>
	[TestMethod]
	public void NotEnoughCharactersDeclarationOfThematicBreak()
	{
		var parser = new MDASTParser();

		var actual = parser.Parse(
			"--\n" +
			"**\n" +
			"__"
		);

		// This will fail eventually when MDASTTextNode parsing is fixed. The parser will read the text as one
		// all-inclusive MDASTTextNode, as https://spec.commonmark.org/0.30/#example-46 shows.
		var expected = new MDASTRootNode();
		expected.Children.AddRange(new List<MDASTNode>
		{
			new MDASTTextNode("--"),
			new MDASTTextNode("**"),
			new MDASTTextNode("__"),
		});

		Assert.AreEqual(expected, actual);
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-47">Thematic Break Example 47</see>
	/// </summary>
	[TestMethod]
	public void AllowedIndentationDeclarationOfThematicBreak()
	{
		var parser = new MDASTParser();

		var actual = parser.Parse(
			" ***\n" +
			"  ***\n" +
			"   ***"
		);

		var expected = new MDASTRootNode();
		expected.Children.AddRange(new List<MDASTNode>
		{
			new MDASTThematicBreakNode(),
			new MDASTThematicBreakNode(),
			new MDASTThematicBreakNode(),
		});

		Assert.AreEqual(expected, actual);
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation & Extension of
	/// <see href="https://spec.commonmark.org/0.30/#example-48">Thematic Break Example 48</see>
	/// and
	/// <see href="https://spec.commonmark.org/0.30/#example-49">Thematic Break Example 49</see>
	/// </summary>
	[TestMethod]
	public void FourOrMoreSpacesIsTooManyForThematicBreak()
	{
		var parser = new MDASTParser();

		var actual = parser.Parse(
			"Foo\n" +
			"    ***\n" +
			"     ***\n" +
			"      ***"	
		);

		// This will fail eventually when MDASTTextNode parsing is fixed. The parser will read the text as one
		// all-inclusive MDASTTextNode, as https://spec.commonmark.org/0.30/#example-49 shows.
		var expected = new MDASTRootNode();
		expected.Children.AddRange(new List<MDASTNode>
		{
			new MDASTTextNode("Foo"),
			new MDASTTextNode("    ***"),
			new MDASTTextNode("     ***"),
			new MDASTTextNode("      ***"),
		});

		Assert.AreEqual(expected, actual);
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-50">Thematic Break Example 50</see>
	/// </summary>
	[TestMethod]
	public void MoreThanThreeValidCharactersMayBeUsedForThematicBreak()
	{
		var parser = new MDASTParser();

		var actual = parser.Parse(
			"____________________________"
		);

		var expected = new MDASTRootNode();
		expected.Children.Add(new MDASTThematicBreakNode());

		Assert.AreEqual(expected, actual);
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-51">Thematic Break Example 51</see>,
	/// <see href="https://spec.commonmark.org/0.30/#example-52">Thematic Break Example 52</see>,
	/// and
	/// <see href="https://spec.commonmark.org/0.30/#example-53">Thematic Break Example 53</see>
	/// </summary>
	[TestMethod]
	public void SpacesAndTabsAreAllowedBetweenTheCharactersForThematicBreak()
	{
		var parser = new MDASTParser();

		var actual = parser.Parse(
			" - - -\n" +
			" **  * ** * ** * **\n" +
			"-     -     -     -"
		);

		var expected = new MDASTRootNode();
		expected.Children.AddRange(new List<MDASTNode>
		{
			new MDASTThematicBreakNode(),
			new MDASTThematicBreakNode(),
			new MDASTThematicBreakNode(),
		});

		Assert.AreEqual(expected, actual);
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-54">Thematic Break Example 54</see>
	/// </summary>
	[TestMethod]
	public void SpacesAndTabsAreAllowedAtTheEndOfThematicBreak()
	{
		var parser = new MDASTParser();
		
		var actual = parser.Parse(
			"- - - -    "
		);

		var expected = new MDASTRootNode();
		expected.Children.Add(new MDASTThematicBreakNode());

		Assert.AreEqual(expected, actual);
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-55">Thematic Break Example 55</see>
	/// </summary>
	[TestMethod]
	public void NoCharactersOtherThanSpacesOrTabsAllowedAtAtTheEndOfThematicBreak()
	{
		var parser = new MDASTParser();

		// This will fail eventually when MDASTTextNode parsing is fixed. Uncommenting the commented lines
		// will fix the issue, since it will be intended that they are separate lines.
		var actual = parser.Parse(
			"_ _ _ _ a\n" +
			/*"\n" +*/
			"a------\n" +
			/*"\n" +*/
			"---a---\n" 
		);

		var expected = new MDASTRootNode();
		expected.Children.AddRange(new List<MDASTNode>
		{
			new MDASTTextNode("_ _ _ _ a"),
			new MDASTTextNode("a------"),
			new MDASTTextNode("---a---"),
		});

		Assert.AreEqual(expected, actual);
	}
}