using Calculator.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
               // Add Variable to dictionary here
               //var mainWindowViewModel = Application.Current.MainWindow.DataContext as MainWindowViewModel;
               // mainWindowViewModel.Variables
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
