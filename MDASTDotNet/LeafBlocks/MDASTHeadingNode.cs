using MDASTDotNet.Extensions;
using Newtonsoft.Json;

namespace MDASTDotNet.LeafBlocks
{
	[JsonObject(MemberSerialization.OptIn)]
	public class MDASTHeadingNode : MDASTNode
	{
		[JsonProperty("level")]
		public int Level { get; set; }

		[JsonConstructor]
		public MDASTHeadingNode(int level) : base("heading")
		{
			this.Level = level;

			if (level < 0)
			{
				throw new ArgumentException("Heading Level cannot be less than 0.");
			}

			if (level > 6)
			{
				throw new ArgumentException("Heading level cannot be greater than 6.");
			}
		}

		internal enum ParsingState
		{
			HeadingDeclaration,
			RequiredSpaceOrTab,
		}

		internal static MDASTHeadingNode? TryParse(string target)
		{
			var parsingState = ParsingState.HeadingDeclaration;

			var headerLevel = 0;

			int i = 0;
			do
			{
				char current = target[i];

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

			return new MDASTHeadingNode(headerLevel);
		}
	}
}
