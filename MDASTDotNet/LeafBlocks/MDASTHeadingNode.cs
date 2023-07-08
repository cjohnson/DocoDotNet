using MDASTDotNet.Extensions;
using Newtonsoft.Json;

namespace MDASTDotNet.LeafBlocks
{
	[JsonObject(MemberSerialization.OptIn)]
	public class MDASTHeadingNode : MDASTNode
	{
		[JsonProperty("level")]
		public int Level { get; set; }

		[JsonProperty("text")]
		public MDASTTextNode? Text { get; set; }

		[JsonConstructor]
		public MDASTHeadingNode(int level, MDASTTextNode? text) : base("heading")
		{
			this.Level = level;
			this.Text = text;

			if (level < 0)
			{
				throw new ArgumentException("Heading Level cannot be less than 0.");
			}

			if (level > 6)
			{
				throw new ArgumentException("Heading level cannot be greater than 6.");
			}
		}

		public override bool Equals(object? obj)
		{
			return obj is MDASTHeadingNode node &&
				   Level == node.Level &&
				   EqualityComparer<MDASTTextNode?>.Default.Equals(Text, node.Text);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Type, Level, Text);
		}

		internal enum ParsingState
		{
			Indentation,
			HeadingDeclaration,
			RequiredSpaceOrTab,
		}

		internal static MDASTHeadingNode? TryParse(string target)
		{
			var parsingState = ParsingState.Indentation;
			var indentationCount = 0;
			var headerLevel = 0;

			if (target[0] == '\\')
			{
				return null;
			}

			int i = 0;
			do
			{
				char current = target[i];

				if (parsingState == ParsingState.Indentation)
				{
					if (current == '#')
					{
						parsingState = ParsingState.HeadingDeclaration;
						continue;
					}

					if (!Char.IsWhiteSpace(current))
					{
						return null;
					}

					++indentationCount;
					if (indentationCount > 3)
					{
						return null;
					}

					++i;
					continue;
				}

				if (parsingState == ParsingState.HeadingDeclaration)
				{
					if (current.MatchesAny(' ', '\t'))
					{
						parsingState = ParsingState.RequiredSpaceOrTab;
						continue;
					}

					if (current != '#')
					{
						return null;
					}

					++headerLevel;

					if (headerLevel > 6)
					{
						return null;
					}
						
					++i;
					continue;
				}

				if (parsingState == ParsingState.RequiredSpaceOrTab)
				{
					if (!current.MatchesAny(' ', '\t'))
					{
						return null;
					}

					break;
				}

			} while (i < target.Length);

			if (parsingState == ParsingState.HeadingDeclaration)
			{
				return new MDASTHeadingNode(headerLevel, null);
			}

			var textContent = target.Substring(i + 1, target.Length - i - 1);
			var text = new MDASTTextNode(textContent);

			return new MDASTHeadingNode(headerLevel, text);
		}
	}
}
