﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MasterApriori.Entities;

namespace MasterApriori.Windows
{
	/// <summary>
	/// Interaction logic for ResultWindow.xaml
	/// </summary>
	public partial class ResultWindow : Window
	{
		private List<Result> results;
		private List<object> list;

		public ResultWindow(List<Result> results)
		{
			InitializeComponent();
			this.results = results;

			list = new List<object>();

			foreach (var res in results)
			{
				list.Add(new
				{
					Transation = res.Name,
					Seconds = Math.Ceiling((double)(res.Stopwatch.ElapsedMilliseconds / 1000)),
					TrasactionCount = res.TrasactionCount,
					RulesCount = res.Output.StrongRules.Count
				});
			}

			DataGrid1.AutoGeneratedColumns += datagrid1_AutoGeneratedColumns;

			DataGrid1.ItemsSource = list;
		}

		private void TimeChart_OnLoaded(object sender, RoutedEventArgs e)
		{
			((ColumnSeries) TimeChart.Series[0]).ItemsSource = list;
		}

		private void TransactionsChart_OnLoaded(object sender, RoutedEventArgs e)
		{

			((LineSeries)TransactionsChart.Series[0]).ItemsSource = list;

		}

		private void RulesChart_OnLoaded(object sender, RoutedEventArgs e)
		{
			((PieSeries)RulesChart.Series[0]).ItemsSource = list;
		}



		void datagrid1_AutoGeneratedColumns(object sender, EventArgs e)
		{
			DataGrid1.Columns[0].Header = "Ім'я транзакції";
			DataGrid1.Columns[1].Header = "Час виконання";
			DataGrid1.Columns[2].Header = "Кількість транзакцій";
			DataGrid1.Columns[3].Header = "Кількість правил";
		}
	}
}