﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using MasterApriori.Contracts;
using MasterApriori.Entities;

namespace MasterApriori.Implementation
{
	// Delegate that defines the signature for the callback method.
	//
	public delegate void ExampleCallback(string result);

	// The ThreadWithState class contains the information needed for
	// a task, the method that executes the task, and a delegate
	// to call when the task is complete.
	//
	public class AprioriInThread
	{
		// State information used in the task.
		private float minSupport;
		private float minConfidence;
		private float minLift;
		private IEnumerable<string> items; 
		private string[][] transactions;
		private string[] itemsD;

		// The constructor obtains the state information and the
		// callback delegate.
		public AprioriInThread(float minSupport, float minConfidence, float minLift, IEnumerable<string> items, 
			string[][] transactions, string[] itemsD)
		{
			this.minSupport = minSupport;
			this.minConfidence = minConfidence;
			this.minLift = minLift;
			this.items = items;
			this.transactions = transactions;
			this.itemsD = itemsD;
		}

		// The thread procedure performs the task, such as
		// formatting and printing a document, and then invokes
		// the callback delegate with the number of lines printed.
		public async Task<Result> ThreadProc()
		{
			IApriori apriori = new Apriori();
			var stopWatch = new Stopwatch();
			stopWatch.Start();
			var result = apriori.ProcessTransaction((float)minSupport, (float)minConfidence, (float)minLift, items, transactions, itemsD);
			stopWatch.Stop();



			return new Result(result, stopWatch);
		}
	}
}
