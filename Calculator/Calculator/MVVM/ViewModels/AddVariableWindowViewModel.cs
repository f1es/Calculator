using Calculator.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Calculator.MVVM.ViewModels
{
    public class AddVariableWindowViewModel : ObservableObject
    {
        private string _variableExpression;
        public string VariableExpression
        {
            get => _variableExpression;
            set
            {
                _variableExpression = value;
                OnPropertyChanged();
            }
        }
        public ICommand AcceptCommand
        {
            get => new RelayCommand(c =>
            {
                var mainWindowViewModel = Application.Current.MainWindow.DataContext as MainWindowViewModel;
                try
                {
                    mainWindowViewModel.PostfixCalculator.Variables.Calculate(VariableExpression);
					CollectionViewSource.GetDefaultView(mainWindowViewModel.Variables).Refresh();
                    mainWindowViewModel.OnPropertyChanged("Variables");
                    WindowsHelper.CloseWindow(this);
				}
                catch(Exception ex)
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
