using Calculator.MVVM.Models;
using Calculator.MVVM.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml;

namespace Calculator.MVVM.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private string _expression = "";
        private PostfixCalculator _postfixCalculator = new PostfixCalculator();
        public Dictionary<string, double> Variables 
        {
            get => _postfixCalculator.Variables.Variables;
        }
        public Dictionary<string, (string[] Parameters, string Body, Func<double[], double> Function, string BodyWithHeader)> Functions
        {
            get => _postfixCalculator.Functions.Functions;
        }
        public Dictionary<string, string> ViewFunctions
        {
            get => _postfixCalculator.Functions.ViewFunctions;
        }
        public string Expression
        {
            get => _expression.ToString();
            set
            {
                _expression = value;
                OnPropertyChanged();
            }
        }
        public PostfixCalculator PostfixCalculator
        {
            get => _postfixCalculator;
            private set => _postfixCalculator = value;
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
                Expression += "sqrt()";
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
                if (Expression.Length > 0)
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
                Expression += ".";
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
                    char dot = '.';
                    char comma = ',';
                    Expression = _postfixCalculator.CalculatePostfix(postfixExpression).ToString().Replace(comma, dot);
                }
                catch (Exception ex) 
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }
        public ICommand RemoveVariableCommand 
        { 
            get => new RelayCommand(RemoveVariable); 
        }
        public ICommand RemoveFunctionCommand
        {
            get => new RelayCommand(RemoveFunction);
        }
        public ICommand AddVariableCommand
        {
            get => new RelayCommand(c =>
            {
				if (!WindowsHelper.IsWindowExists(typeof(AddVariableWindow)))
				{
					new AddVariableWindow().Show();
					CollectionViewSource.GetDefaultView(Variables).Refresh();
				}
			});
        }
        public ICommand AddFunctionCommand
        {
            get => new RelayCommand(c =>
            {
                if (!WindowsHelper.IsWindowExists(typeof(AddFunctionWindow)))
                {
                    new AddFunctionWindow().Show();
                }
            });
        }
        
		private void RemoveVariable(object key)
		{
			if (key is string keyValue)
			{
				if (Variables.ContainsKey(keyValue))
				{
                    PostfixCalculator.Variables.RemoveVariable(keyValue);
                    OnPropertyChanged("Variables");
				}
			}
		}
        
        private void RemoveFunction(object key)
        {
			if (key is string keyValue)
			{
				if (Functions.ContainsKey(keyValue))
				{
					PostfixCalculator.Functions.RemoveFunction(keyValue);
					OnPropertyChanged("Functions");
					OnPropertyChanged("ViewFunctions");
					CollectionViewSource.GetDefaultView(_postfixCalculator.Functions.Functions).Refresh();
				}
			}
		}
	}
}
