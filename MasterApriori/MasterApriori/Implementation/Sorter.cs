using System.Linq;
using MasterApriori.Contracts;
using MasterApriori.Utils;

namespace MasterApriori.Implementation
{
	public class Sorter : ISorter
	{
		public string Sort(string token)
		{
			var items = token.Split(Constants.ITEM_SEPARATOR);
			items.OrderBy(x => x);
			return string.Join(Constants.ITEM_SEPARATOR.ToString(), items.OrderBy(k => k));
		}
	}
}
