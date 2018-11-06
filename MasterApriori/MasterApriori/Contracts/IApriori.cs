using MasterApriori.Entities;

namespace MasterApriori.Contracts
{
	using System.Collections.Generic;

	public interface IApriori
	{
		void SetMinLift(double value);
		Output ProcessTransaction(double minSupport, double minConfidence, IEnumerable<string> items, string[][] transactions, string[] itemsD = null);
	}
}