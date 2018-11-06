using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using JulMar.Core.Extensions;
using MasterApriori.Entities;

namespace MasterApriori.Utils
{
	public class ItemsDictionary : KeyedCollection<string, Item>
	{
		private static List<string> addedItems = new List<string>();
		protected override string GetKeyForItem(Item item)
		{
			return string.Join("", item.Names);
		}

		internal void ConcatItems(IList<Item> frequentItems)
		{
			
			foreach (var item in frequentItems)
			{
				var itemsStr = string.Join("", item.Names.OrderBy(x => x));
				if (!addedItems.Any(x => x == itemsStr))
				{
					this.Add(item);
					addedItems.Add(itemsStr);
				}
			}
		}
	}
}