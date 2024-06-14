using Calculator.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Calculator.MVVM.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private string _expression = "";
        private PostfixCalculator _postfixCalculator = new PostfixCalculator();

        public string Expression
        {
            get => _expression.ToString(); 
            set
            {
                _expression = value;
                OnPropertyChanged();
            }
        }
        public ICommand CloseCommand
        {
            get => new RelayCommand(c =>
            {
                WindowsHelper.CloseWindow(this);
            });
        }
        public ICommand MinimizeCommand
        {
            get => new RelayCommand(c =>
            {
                WindowsHelper.MinimizeWindow(this);
            });
        }
        public ICommand ClearExpressionCommand
        {
            get => new RelayCommand(c =>
            {
                Expression = "";
            });
        }
        public ICommand PrintOpenBracketCommand
        {
            get => new RelayCommand(c =>
            {
                Expression += "(";
            });
        }
        public ICommand PrintCloseBracketCommand
        {
            get => new RelayCommand(_ =>
            {
                Expression += ")";
            });
        }

        public ICommand CalculateCommand
        {
            get => new RelayCommand(c =>
            {
                try
                {
                    PostfixExpression postfixExpression = new PostfixExpression(Expression);
                    Expression = "";
                    Expression = _postfixCalculator.CalculatePostfix(postfixExpression).ToString();
                }
                catch (Exception ex) 
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }
    }
}
