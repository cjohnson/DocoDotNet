﻿using Newtonsoft.Json;

using MDASTDotNet.Extensions;

namespace MDASTDotNet.LeafBlocks;

/// <summary>
/// A Thematic Break node represents a thematic break (traditionally a &lt;br/&gt; tag in HTML).
/// </summary>
[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
public class ThematicBreakNode : INode
{
	public string Type { get; init; } = "thematicBreak";

	public ThematicBreakNode()
	{ }

	internal enum ParsingState
	{
		Indentation,
		MatchCharacter,
	}

	public override bool Equals(object? obj)
	{
		return obj is ThematicBreakNode;
	}

	public override int GetHashCode()
	{
		return Type.GetHashCode();
	}

	internal static ThematicBreakNode? TryParse(string target)
	{
		var parsingState = ParsingState.Indentation;

		var indentationCount = 0;

		char? selectedCharacter = null;
		var selectedCharacterCount = 0;

		int i = 0;
		do
		{
			char current = target[i];

			if (parsingState == ParsingState.Indentation)
			{
				if (indentationCount > 3)
				{
					return null;
				}

				if (current.MatchesAny('-', '_', '*'))
				{
					parsingState = ParsingState.MatchCharacter;
					continue;
				}

				if (!current.MatchesAny('\t', ' '))
				{
					return null;
				}

				++indentationCount;
				++i;
				continue;
			}

			if (parsingState == ParsingState.MatchCharacter)
			{
				if (selectedCharacter == null)
				{
					if (!current.MatchesAny('-', '_', '*'))
					{
						return null;
					}

					selectedCharacter = current;
				}

				if (!current.MatchesAny(' ', '\t', (char)selectedCharacter))
				{
					return null;
				}

				if (current == selectedCharacter)
				{
					++selectedCharacterCount;
				}

				++i;
			}
		} while (i < target.Length);

		if (selectedCharacterCount < 3)
		{
			return null;
		}

		return new ThematicBreakNode();
	}
}