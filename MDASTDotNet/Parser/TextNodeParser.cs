using MDASTDotNet.LeafBlocks;

namespace MDASTDotNet.Parser;

public class TextNodeParser : IParser
{
	public INode? Parse(List<string> contentLines)
	{
		if (!contentLines.Any())
		{
			return null;
		}

		var text = new TextNode(contentLines.First());
		contentLines.RemoveAt(0);
		return text;
	}
}
