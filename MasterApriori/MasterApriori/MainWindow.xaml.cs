using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using MasterApriori.Contracts;
using MasterApriori.Entities;
using MasterApriori.FileReader;
using MasterApriori.Implementation;
using Microsoft.Win32;

namespace MasterApriori
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private string[][] transactions;
		private string[] items;
		private string[] itemsD;
		private string result = "";
		private IApriori apriori = new Apriori();

		public MainWindow()
		{
			InitializeComponent();
		}

		private void uploadDataset_Click(object sender, RoutedEventArgs e)
		{
			var dialog = new OpenFileDialog
			{
				CheckFileExists = true,
				CheckPathExists = true,
				Title = "Choose file with transactions",
				Multiselect = false
			};
			if (dialog.ShowDialog(this) != true) return;
			transactions = AFileReader.ReadFromFile(dialog.FileName, Encoding.UTF8);
			TextResult.Text = "Dataset uploaded!";
		}

		private void uploadItems_Click(object sender, RoutedEventArgs e)
		{
			var dialog = new OpenFileDialog
			{
				CheckFileExists = true,
				CheckPathExists = true,
				Title = "Choose file with items",
				Multiselect = false
			};
			if (dialog.ShowDialog(this) != true) return;
			items = AFileReader.ReadFromFile(dialog.FileName, Encoding.Default).Select(x => x[0]).ToArray();
			TextResult.Text = "Items uploaded!";
		}

		private void uploadItemsD_Click(object sender, RoutedEventArgs e)
		{
			var dialog = new OpenFileDialog
			{
				CheckFileExists = true,
				CheckPathExists = true,
				Title = "Choose file with itemsD",
				Multiselect = false
			};
			if (dialog.ShowDialog(this) != true) return;
			itemsD = AFileReader.ReadFromFile(dialog.FileName, Encoding.Default).Select(x => x[0]).ToArray();
			TextResult.Text = "ItemsD uploaded!";
		}

		private void process_Click(object sender, RoutedEventArgs e)
		{
			Stopwatch stopWatch = new Stopwatch();
			var minSupport = 0.4;
			var minConfidence = 0.5;
			var minLift = 0.0;
			Double.TryParse(MinSupportTextBox.Text, out minSupport);
			Double.TryParse(MinConfidenceTextBox.Text, out minConfidence);
			Double.TryParse(MinLiftTextBox.Text, out minLift);

			if (minLift > 0)
			{
				apriori.SetMinLift(minLift);
			}

			stopWatch.Start();
			var result = apriori.ProcessTransaction(minSupport, minConfidence, items, transactions, itemsD);
			stopWatch.Stop();



			TextResult.Text = GetResult(result, stopWatch);
		}


		private bool ValidateItems()
		{
			if (transactions == null || transactions.Length == 0 || items == null || items.Length == 0)
			{
				return false;
			}
			return true;
		}

		private string GetResult(Output result, Stopwatch stopWatch)
		{
			var resultString = "Frequent items:\n";
			foreach (var frequentItem in result.FrequentItems)
			{
				resultString += $"Name: { string.Join(" ", frequentItem.Names)}, Support: {frequentItem.Support}\n";
			}

			resultString += $"Found {result.StrongRules.Count} strong rules:\n";
			foreach (var strongRule in result.StrongRules)
			{
				resultString += $"Rule: {{{string.Join(" ", strongRule.X)} -> {string.Join(" ", strongRule.Y)}}}, confidence: {strongRule.Confidence}\n";
			}
			resultString += $"Processing time: {stopWatch.Elapsed}\n";
			resultString += $"All transactions: {transactions.Length}\n";
			return resultString;
		}
	}
}
