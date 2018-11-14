using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MasterApriori.Contracts;
using MasterApriori.FileReader;
using MasterApriori.Implementation;
using MasterApriori.Utils;
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
			var minSupport = 0.4;
			var minConfidence = 0.5;
			var minLift = 0.0;
			Double.TryParse(MinSupportTextBox.Text, out minSupport);
			Double.TryParse(MinConfidenceTextBox.Text, out minConfidence);
			Double.TryParse(MinLiftTextBox.Text, out minLift);

			AprioriInThread tws = new AprioriInThread((float)minSupport, (float)minConfidence, (float)minLift, items, transactions, itemsD);
			TextResult.Text = "LOADING...";

			TextResult.Text = await Task.Run(tws.ThreadProc);
		}


		private bool ValidateItems()
		{
			if (transactions == null || transactions.Length == 0 || items == null || items.Length == 0)
			{
				return false;
			}
			return true;
		}
	}
}
