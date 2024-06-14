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
        public ICommand PrintDivideCommand
        {
            get => new RelayCommand(_ =>
            {
                Expression += "/";
            });
        }
        public ICommand PrintSquareRootCommand
        {
            get => new RelayCommand(_ =>
            {
                Expression += "√";
            });
        }
        public ICommand PrintPowCommand
        {
            get => new RelayCommand(_ =>
            {
                Expression += "^";
            });
        }
        public ICommand DeleteLastSymbolCommand
        {
            get => new RelayCommand(_ =>
            {
                Expression = Expression.Remove(Expression.Length - 1);
            });
        }
        public ICommand PrintMultipleCommand
        {
            get => new RelayCommand(_ =>
            {
                Expression += "*";
            });
        }
        public ICommand PrintOneCommand
        {
            get => new RelayCommand(_ =>
            {
                Expression += "1";
            });
        }
        public ICommand PrintTwoCommand
        {
            get => new RelayCommand(_ =>
            {
                Expression += "2";
            });
        }
        public ICommand PrintThreeCommand
        {
            get => new RelayCommand(_ =>
            {
                Expression += "3";
            });
        }
        public ICommand PrintFourCommand
        {
            get => new RelayCommand(_ =>
            {
                Expression += "4";
            });
        }
        public ICommand PrintFiveCommand
        {
            get => new RelayCommand(_ =>
            {
                Expression += "5";
            });
        }
        public ICommand PrintSixCommand
        {
            get => new RelayCommand(_ =>
            {
                Expression += "6";
            });
        }
        public ICommand PrintSevenCommand
        {
            get => new RelayCommand(_ =>
            {
                Expression += "7";
            });
        }
        public ICommand PrintEightCommand
        {
            get => new RelayCommand(_ =>
            {
                Expression += "8";
            });
        }
        public ICommand PrintNineCommand
        {
            get => new RelayCommand(_ =>
            {
                Expression += "9";
            });
        }
        public ICommand PrintZeroCommand
        {
            get => new RelayCommand(_ =>
            {
                Expression += "0";
            });
        }
        public ICommand PrintDotCommand
        {
            get => new RelayCommand(_ =>
            {
                Expression += ",";
            });
        }
        public ICommand PrintPlusCommand
        {
            get => new RelayCommand(_ =>
            {
                Expression += "+";
            });
        }
        public ICommand PrintMinusCommand
        {
            get => new RelayCommand(_ =>
            {
                Expression += "-";
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
