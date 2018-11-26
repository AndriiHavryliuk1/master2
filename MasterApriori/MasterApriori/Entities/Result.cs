using System.Diagnostics;

namespace MasterApriori.Entities
{
	public class Result
	{
		public Result()
		{
			
		}

		public Result(Output output, Stopwatch stopwatch)
		{
			Output = output;
			Stopwatch = stopwatch;
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public Output Output { get; set; }
		public Stopwatch Stopwatch { get; set; }
		public int TrasactionCount { get; set; }
	}
}
