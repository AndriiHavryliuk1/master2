using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MasterApriori.Contracts;
using MasterApriori.Entities;
using MasterApriori.FileReader;
using MasterApriori.Implementation;
using MasterApriori.Windows;
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
		public List<Result> Results;

		public MainWindow()
		{
			InitializeComponent();
			Results = new List<Result>();
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
			TextResult.Text = "Транзакції завантажено успішно!";
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
			TextResult.Text = "Характеристики завантажено успішно!";
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
			TextResult.Text = "Характеристики що описують ДТП завантажено успішно!";
		}

		private async void process_Click(object sender, RoutedEventArgs e)
		{
			if (!Validation())
			{
				return;
			}

			loadingGif.Visibility = Visibility.Visible;
			richTextBox.Visibility = Visibility.Hidden;
			var minSupport = 0.4;
			var minConfidence = 0.5;
			var minLift = 0.0;
			Double.TryParse(MinSupportTextBox.Text, out minSupport);
			Double.TryParse(MinConfidenceTextBox.Text, out minConfidence);
			Double.TryParse(MinLiftTextBox.Text, out minLift);

			AprioriInThread tws = new AprioriInThread((float)minSupport, (float)minConfidence, (float)minLift, items, transactions, itemsD);
			TextResult.Text = "LOADING...";

			var result = await Task.Run(tws.ThreadProc);

			result.Id = Results.Count > 0 ? Results.OrderBy(x => x.Id).Last().Id + 1 : 0;
			result.Name = "Транзакція 1";
			result.TrasactionCount = transactions.Length;
			Results.Add(result);
			TextResult.Text = GetResult(result.Output, result.Stopwatch);
			loadingGif.Visibility = Visibility.Hidden;
			richTextBox.Visibility = Visibility.Visible;
		}


		private string GetResult(Output result, Stopwatch stopWatch)
		{
			var resultString = "Список частих характеристик:\n";
			foreach (var frequentItem in result.FrequentItems)
			{
				resultString += $"Назва: { string.Join(" ", frequentItem.Names)}, підтримка: {frequentItem.Support}\n";
			}

			resultString += $"Знайдено {result.StrongRules.Count} правил:\n";
			foreach (var strongRule in result.StrongRules)
			{
				resultString += $"Правило: {{{string.Join(" ", strongRule.X)} -> {string.Join(" ", strongRule.Y)}}}, вірогідність: {strongRule.Confidence}\n";
			}
			resultString += $"Час виконання: {stopWatch.Elapsed}\n";
			resultString += $"Кількість транзакцій: {transactions.Length}\n";
			return resultString;
		}


		private bool Validation()
		{

			if (transactions == null || transactions.Length == 0)
			{
				MessageBox.Show("Список транзакцій не завантажено!", "Помилка!");
				return false;
			}
			if (items == null || items.Length == 0)
			{
				MessageBox.Show("Список характеристик не завантажено!", "Помилка!");
				return false;
			}
			return true;

		}

		private void button_Click(object sender, RoutedEventArgs e)
		{
			var resultWindow = new ResultWindow(Results);
			resultWindow.Show();
		}
	}
}
