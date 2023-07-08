using MDASTDotNet.Extensions;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

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
			var headingRegex = new Regex("^(?: |\t){0,3}(#{1,6})(?: |\t)+(.*?)(?: |\t)*$", RegexOptions.Multiline);

			var match = headingRegex.Match(target);
			if (!match.Success)
			{
				return null;
			}

			var level = match.Groups[1].Value.Length;
			var text = new MDASTTextNode(match.Groups[2].Value);

			return new MDASTHeadingNode(level, text);
		}
	}
}
