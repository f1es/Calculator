using Calculator.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Calculator.MVVM.ViewModels
{
    public class AddFunctionWindowViewModel : ObservableObject
    {
		private string _functionExpression;
		public string FunctionExpression
		{
			get => _functionExpression;
			set
			{
				_functionExpression = value;
				OnPropertyChanged();
			}
		}

		public ICommand AcceptCommand
		{
			get => new RelayCommand(c =>
			{
				// Add function to dictionary here
				try
				{
					var mainWindowViewModel = Application.Current.MainWindow.DataContext as MainWindowViewModel;
					mainWindowViewModel.PostfixCalculator.Functions.ParseFunction(FunctionExpression);
					CollectionViewSource.GetDefaultView(mainWindowViewModel.Functions).Refresh();
					CollectionViewSource.GetDefaultView(mainWindowViewModel.ViewFunctions).Refresh();
					mainWindowViewModel.OnPropertyChanged("ViewFunctions");
					WindowsHelper.CloseWindow(this);
				}
				catch (Exception ex)
				{ 
				 MessageBox.Show(ex.Message);
				}
				
			});
		}

		public ICommand CloseCommand
		{
			get => new RelayCommand(c =>
			{
				WindowsHelper.CloseWindow(this);
			});
		}
	}
}
