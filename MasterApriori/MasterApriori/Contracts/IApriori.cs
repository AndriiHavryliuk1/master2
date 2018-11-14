using MasterApriori.Entities;

namespace MasterApriori.Contracts
{
	using System.Collections.Generic;

	public interface IApriori
	{
		void SetMinLift(double value);
		Output ProcessTransaction(float minSupport, float minConfidence, float minLift, IEnumerable<string> items, string[][] transactions, string[] itemsD = null);
	}
}