using MasterApriori.Utils;

namespace MasterApriori.Entities
{
	using System.Collections.Generic;

	public class Output
	{
		public IList<Rule> StrongRules { get; set; }

		public ItemsDictionary FrequentItems { get; set; }
	}
}