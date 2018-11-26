using MasterApriori.Contracts;
using MasterApriori.Entities;
using System.Collections.Generic;
using System.Linq;
using MasterApriori.Utils;

namespace MasterApriori.Implementation
{
	public class Apriori : IApriori
	{
		private double minSupport;
		private double minConfidence;
		private double minLift = 0.0;
		private long transactionsCount = 0;

		public double MinSupport
		{
			get { return minSupport; }
			set { minSupport = value; }
		}

		public double MinConfidence
		{
			get { return minConfidence; }
			set { minConfidence = value; }
		}

		public void SetMinLift(double value)
		{
			minLift = value;
		}

		Output IApriori.ProcessTransaction(float minSupport, float minConfidence, float minLift, IEnumerable<string> items, string[][] transactions, string[] itemsD = null)
		{
			this.minSupport = minSupport;
			this.minConfidence = minConfidence;
			this.transactionsCount = transactions.Length;
			this.minLift = minLift;
			IList<Item> frequentItems = GetL1FrequentItems(items, transactions);
			ItemsDictionary allFrequentItems = new ItemsDictionary();
			allFrequentItems.ConcatItems(frequentItems);
			var candidates = new Dictionary<string[], double>();
			double transactionsCount = transactions.Count();

			do
			{
				candidates = GenerateCandidates(frequentItems, transactions, itemsD);
				frequentItems = GetFrequentItems(candidates, transactionsCount);
				allFrequentItems.ConcatItems(frequentItems);
			} while (candidates.Count != 0);

			HashSet<Rule> rules = GenerateRules(allFrequentItems);
			IList<Rule> strongRules = GetStrongRules(minConfidence, rules, allFrequentItems);

			return new Output
			{
				StrongRules = strongRules,
				FrequentItems = allFrequentItems
			};
		}

		private List<Item> GetL1FrequentItems(IEnumerable<string> items, string[][] transactions)
		{
			var frequentItemsL1 = new List<Item>();
			double transactionsCount = transactions.Count();

			foreach (var item in items)
			{
				double support = GetSupport(new [] {item}, transactions);

				if (support / transactionsCount >= minSupport)
				{
					frequentItemsL1.Add(new Item { Names = new [] { item }, Support = support });
				}
			}
		//	frequentItemsL1.Sort();
			return frequentItemsL1;
		}

		private double GetSupport(string[] generatedCandidates, IEnumerable<IEnumerable<string>> transactionsList)
		{
			double support = 0;

			foreach (var transactions in transactionsList)
			{
				if (!generatedCandidates.Except(transactions).Any())
				{
					support++;
				}
			}

			return support;
		}


		private Dictionary<string[], double> GenerateCandidates(IList<Item> frequentItems, IEnumerable<IEnumerable<string>> transactions, string[] itemsD)
		{
			var candidates = new Dictionary<string[], double>();

			for (var i = 0; i < frequentItems.Count - 1; i++)
			{
				var firstItems = frequentItems[i].Names.OrderBy(x => x).ToArray();
				
				for (var j = i + 1; j < frequentItems.Count; j++)
				{
					var secondItems = frequentItems[j].Names.OrderBy(x => x).ToArray();
					var generatedCandidate = GenerateCandidate(firstItems, secondItems);

					if (generatedCandidate == null)
					{
						continue;
					}


					double support;
					if (itemsD == null)
					{
						support = GetSupport(generatedCandidate, transactions);
						candidates.Add(generatedCandidate, support);
					}
					else
					{
						if (itemsD.Any(itemD => generatedCandidate.Contains(itemD)))
						{
							support = GetSupport(generatedCandidate, transactions);
							candidates.Add(generatedCandidate, support);
						}
					}
				}

			}

			return candidates;
		}

		private string[] GenerateCandidate(string[] firstItems, string[] secondItems)
		{
			if (firstItems.Length == 1)
			{
				return firstItems.Concat(secondItems).Distinct().ToArray();
			}
			else
			{
				var firstSubArray = firstItems.Take(firstItems.Length - 1);
				var secondSubArray = secondItems.Take(secondItems.Length - 1);

				if (string.Join("", firstSubArray) == string.Join("", secondSubArray))
				{
					return firstItems.Concat(new [] { secondItems[secondItems.Length - 1] }).ToArray();
				}
				return null;
			}
		}

		private List<Item> GetFrequentItems(IDictionary<string[], double> candidates, double transactionsCount)
		{
			var frequentItems = new List<Item>();

			foreach (var item in candidates)
			{
				if (item.Value / transactionsCount >= minSupport)
				{
					frequentItems.Add(new Item { Names = item.Key, Support = item.Value });
				}
			}

			return frequentItems;
		}


		private HashSet<Rule> GenerateRules(ItemsDictionary allFrequentItems)
		{
			var rulesList = new HashSet<Rule>();

			foreach (var item in allFrequentItems)
			{
				if (item.Names.Length <= 1) continue;
				IEnumerable<string> subsetsList = item.Names;

				foreach (var subset in subsetsList)
				{
					string[] remaining = GetRemaining(subset, item.Names);
					Rule rule = new Rule(new [] { subset }, remaining, 0);

					if (!rulesList.Contains(rule))
					{
						rulesList.Add(rule);
					}
				}
			}

			return rulesList;
		}

		private string[] GetRemaining(string child, string[] parent)
		{
			return parent.Where(x => x != child).ToArray();
		}

		private IList<Rule> GetStrongRules(double minConfidence, HashSet<Rule> rules, ItemsDictionary allFrequentItems)
		{
			var strongRules = new List<Rule>();

			foreach (Rule rule in rules)
			{
				var xy = rule.X.Concat(rule.Y).ToArray();
				AddStrongRule(rule, xy, strongRules, minConfidence, allFrequentItems);
			}

			return strongRules;
		}

		private void AddStrongRule(Rule rule, string[] XY, List<Rule> strongRules, double minConfidence, ItemsDictionary allFrequentItems)
		{
			double confidence = GetConfidence(rule.X, XY, allFrequentItems);
			double lift = minLift > 0.0 ? GetLift(confidence, rule.Y, allFrequentItems) : 1;

			if (confidence >= minConfidence && lift > minLift)
			{
				Rule newRule = new Rule(rule.X, rule.Y, confidence);
				strongRules.Add(newRule);
			}
		}

		private double GetConfidence(string[] X, string[] XY, ItemsDictionary allFrequentItems)
		{
			var XYstring= string.Join("", XY.OrderBy(x => x));
			var Xstring = string.Join("", X.OrderBy(x => x));
			var supportX = allFrequentItems.FirstOrDefault(x => string.Join("", x.Names.OrderBy(k => k)) == Xstring)?.Support ?? 0;
			var supportXY = allFrequentItems.FirstOrDefault(x => string.Join("", x.Names.OrderBy(k => k)) == XYstring)?.Support ?? 0;
			return supportX > 0 && supportXY > 0 ? supportXY / supportX : 0;
		}

		private double GetLift(double XYConfidence, string[] Y, ItemsDictionary allFrequentItems)
		{
			var Ystring = string.Join("", Y.OrderBy(x => x));
			var supportY = allFrequentItems.FirstOrDefault(x => string.Join("", x.Names.OrderBy(k => k)) == Ystring).Support;
			return XYConfidence / (supportY / this.transactionsCount);
		}
	}
}