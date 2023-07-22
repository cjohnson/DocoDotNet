using MDASTDotNet.LeafBlocks;
using MDASTDotNet.Parser;

namespace MDASTDotNet.Test;

/// <summary>
/// Tests for <see cref="MarkdownParser"/> parsing of <see cref="MDASTThematicBreakNode"/> nodes.
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
		var parser = new MarkdownParser();

		var actual = parser.Parse(
			"***\n" +
			"---\n" +
			"___"
		);

		var expected = new MDASTRootNode();
		expected.Children.AddRange(new List<INode>
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
		var parser = new MarkdownParser();

		var actual = parser.Parse(
			"+++\n" +
			"==="
		);

		// This will fail eventually when MDASTTextNode parsing is fixed. The parser will read the text as one
		// all-inclusive MDASTTextNode, with text "+++\n===".
		var expected = new MDASTRootNode();
		expected.Children.AddRange(new List<INode>
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
		var parser = new MarkdownParser();

		var actual = parser.Parse(
			"--\n" +
			"**\n" +
			"__"
		);

		// This will fail eventually when MDASTTextNode parsing is fixed. The parser will read the text as one
		// all-inclusive MDASTTextNode, as https://spec.commonmark.org/0.30/#example-46 shows.
		var expected = new MDASTRootNode();
		expected.Children.AddRange(new List<INode>
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
		var parser = new MarkdownParser();

		var actual = parser.Parse(
			" ***\n" +
			"  ***\n" +
			"   ***"
		);

		var expected = new MDASTRootNode();
		expected.Children.AddRange(new List<INode>
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
		var parser = new MarkdownParser();

		var actual = parser.Parse(
			"Foo\n" +
			"    ***\n" +
			"     ***\n" +
			"      ***"	
		);

		// This will fail eventually when MDASTTextNode parsing is fixed. The parser will read the text as one
		// all-inclusive MDASTTextNode, as https://spec.commonmark.org/0.30/#example-49 shows.
		var expected = new MDASTRootNode();
		expected.Children.AddRange(new List<INode>
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
		var parser = new MarkdownParser();

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
		var parser = new MarkdownParser();

		var actual = parser.Parse(
			" - - -\n" +
			" **  * ** * ** * **\n" +
			"-     -     -     -"
		);

		var expected = new MDASTRootNode();
		expected.Children.AddRange(new List<INode>
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
		var parser = new MarkdownParser();
		
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
		var parser = new MarkdownParser();

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
		expected.Children.AddRange(new List<INode>
		{
			new MDASTTextNode("_ _ _ _ a"),
			new MDASTTextNode("a------"),
			new MDASTTextNode("---a---"),
		});

		Assert.AreEqual(expected, actual);
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-56">Thematic Break Example 56</see>
	/// </summary>
	[TestMethod]
	public void CharacterChoiceShouldBeConsistentForThematicBreaks()
	{
		var parser = new MarkdownParser();

		var actual = parser.Parse(
			" *-*"
		);

		// This will fail eventually when MDASTEmphasisNode is added. When this fails due to this reason,
		// simply change the output expectation to use MDASTEmphasisNode (or the theoretical equivalent).
		var expected = new MDASTRootNode();
		expected.Children.Add(new MDASTTextNode(" *-*"));

		Assert.AreEqual(expected, actual);
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-57">Thematic Break Example 57</see>
	/// </summary>
	[TestMethod]
	public void BlankLinesBeforeAndAfterAreNotNeededForThematicBreaks()
	{
		var parser = new MarkdownParser();

		var actual = parser.Parse(
			"- foo\n" +
			"***\n" +
			"- bar"
		);

		// This will fail eventually when MDASTListNode is added. When this fails due to this reason,
		// simply change the output expectation to use MDASTListNode (or the theoretical equivalent).
		var expected = new MDASTRootNode();
		expected.Children.AddRange(new List<INode>
		{
			new MDASTTextNode("- foo"),
			new MDASTThematicBreakNode(),
			new MDASTTextNode("- bar"),
		});

		Assert.AreEqual(expected, actual);
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-58">Thematic Break Example 58</see>
	/// </summary>
	[TestMethod]
	public void ParagraphsAreInterruptedByThematicBreaks()
	{
		var parser = new MarkdownParser();

		var actual = parser.Parse(
			"Foo\n" +
			"***\n" +
			"bar"
		);

		var expected = new MDASTRootNode();
		expected.Children.AddRange(new List<INode>
		{
			new MDASTTextNode("Foo"),
			new MDASTThematicBreakNode(),
			new MDASTTextNode("bar"),
		});

		Assert.AreEqual(expected, actual);
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-59">Thematic Break Example 59</see>
	/// </summary>
	[TestMethod]
	public void SetextHeadingsTakePrecedentOverThematicBreaks()
	{
		var parser = new MarkdownParser();

		var actual = parser.Parse(
			"Foo\n" +
			"---\n" +
			"bar"
		);

		// This will fail eventually when Setext Header functionality is added. When this fails due
		// to this reason, simply change the output expectation to use MDASTHeadingNode.
		var expected = new MDASTRootNode();
		expected.Children.AddRange(new List<INode>
		{
			new MDASTTextNode("Foo"),
			new MDASTThematicBreakNode(),
			new MDASTTextNode("bar"),
		});

		Assert.AreEqual(expected, actual);
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-60">Thematic Break Example 60</see>
	/// </summary>
	[TestMethod]
	public void ThematicBreaksTakePrecedentOverLists()
	{
		var parser = new MarkdownParser();

		var actual = parser.Parse(
			"* Foo\n" +
			"* * *\n" +
			"* Bar"
		);

		// This will fail eventually when MDASTListNode is added. When this fails due to this reason,
		// simply change the output expectation to use MDASTListNode (or the theoretical equivalent).
		var expected = new MDASTRootNode();
		expected.Children.AddRange(new List<INode>
		{
			new MDASTTextNode("* Foo"),
			new MDASTThematicBreakNode(),
			new MDASTTextNode("* Bar"),
		});

		Assert.AreEqual(expected, actual);
	}

	/// <summary>
	/// <see href="https://spec.commonmark.org/0.30/">CommonMark 0.30</see>: Implementation of
	/// <see href="https://spec.commonmark.org/0.30/#example-61">Thematic Break Example 61</see>
	/// </summary>
	[TestMethod]
	public void ThematicBreaksInListItemsRequireDifferentCharacterThanTheListBullet()
	{
		var parser = new MarkdownParser();

		var actual = parser.Parse(
			"- Foo\n" +
			"- * * *\n"
		);

		// This will fail eventually when MDASTListNode is added. When this fails due to this reason,
		// simply change the output expectation to use MDASTListNode (or the theoretical equivalent).
		var expected = new MDASTRootNode();
		expected.Children.AddRange(new List<INode>
		{
			new MDASTTextNode("- Foo"), // This will become list -> list item
			new MDASTTextNode("- * * *"), // This will become list -> list item -> thematic break
		});

		Assert.AreEqual(expected, actual);
	}
}