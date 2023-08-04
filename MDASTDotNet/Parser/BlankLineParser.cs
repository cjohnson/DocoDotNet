using MDASTDotNet.LeafBlocks;

namespace MDASTDotNet.Parser;

public class BlankLineParser : IParser
{
	public INode? Parse(List<string> contentLines)
	{
		while (contentLines.Any() && string.IsNullOrWhiteSpace(contentLines.First()))
		{
			contentLines.RemoveAt(0);
		}

		return null;
	}
}
