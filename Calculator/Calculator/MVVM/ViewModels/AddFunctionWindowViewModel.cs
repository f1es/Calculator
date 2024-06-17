using Calculator.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
				//var mainWindowViewModel = Application.Current.MainWindow.DataContext as MainWindowViewModel;
				throw new NotImplementedException();
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
