using Newtonsoft.Json;

using MDASTDotNet.Conversions;

namespace MDASTDotNet
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var parser = new MDASTParser();
			var rootNode = parser.Parse("###\nThat was an empty header!");

			var mdastAsJson = JsonConvert.SerializeObject(rootNode, Formatting.Indented, new JsonSerializerSettings()
			{
				NullValueHandling = NullValueHandling.Ignore,
			});

			Console.WriteLine(mdastAsJson);
        }
	}
}