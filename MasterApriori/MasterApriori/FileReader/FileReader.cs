using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MasterApriori.FileReader
{
	public static class AFileReader
	{
		public static string[][] ReadFromFile(string path, Encoding encoding)
		{
			var text = File.ReadAllText(path, encoding);
			// char[] delimiterChars = { ' ', ',', '.', '\t', '\n', '\\', '\"' };
			text = text.Replace("\r", "");
			//text = text.Replace("\n", "");
			var rows = text.Split('\n');
			var res = new string[rows.Length][];
			for (var i = 0; i < rows.Length; i++)
			{
				var words = rows[i].Split('\"');
				var filteredWords = words.Where(x => x != "").ToArray();
				res[i] = filteredWords;
			}

			return res;
		}
	}
}
