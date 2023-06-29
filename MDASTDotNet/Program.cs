using Newtonsoft.Json;

using MDASTDotNet.Conversions;

namespace MDASTDotNet
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var parser = new MDASTParser();
			var rootNode = parser.Parse("### Level One Header\nSome garbage paragraph");

            Console.WriteLine(JsonConvert.SerializeObject(rootNode, Formatting.Indented));
        }
	}
}