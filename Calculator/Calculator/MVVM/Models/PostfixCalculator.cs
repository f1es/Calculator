using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Calculator.MVVM.Models
{
	public class PostfixCalc
	{
		private Dictionary<string, int> _operatorsPriority = new Dictionary<string, int>()
		{
			{"(", 0},
			{"+", 1},
			{"-", 1},
			{"*", 2},
			{"/", 2},
			{"^", 3},
			{"~", 4}
		};
		private List<string> _operators = new List<string>()
		{
			"+",
			"-",
			"*",
			"/",
			"(",
			")",
			"^",
			"~"
		};
		//private AddVariables _variables = new AddVariables();
		//private AddFunctions _functions = new AddFunctions();
		
		public PostfixCalc()
		{
			//AddFunctions("sqrt(x) = x^1/2");
		}

		private double ExecuteOperation(string op, double first, double second) => op switch
		{
			"+" => first + second,
			"-" => first - second,
			"*" => first * second,
			"/" => first / second,
			"^" => Math.Pow(first, second),
			_ => 0 
		};
		public double CalculatePostfix(PostfixExpression postfixExpression)
		{
			Stack<double> numbers = new Stack<double>();

			foreach (var expressionValue in postfixExpression.Expression)
			{
				switch (expressionValue.ValueType)
				{
					//case ExpressionValueType.Variable:

					//	if (!_variables.Variables.ContainsKey(expressionValue.Value))
					//		throw new Exception("Variable doesn't exist");
					//	numbers.Push(_variables.Variables[expressionValue.Value]);
					//	break;
					case ExpressionValueType.Operator:

						if (expressionValue.Value == "~")
						{
							double lastNumber = 0;
							if (numbers.Count > 0)
								lastNumber = numbers.Pop();
							numbers.Push(-lastNumber);
							break;
						}

						double secondNumber = 0;
                        if (numbers.Count > 0)
							secondNumber = numbers.Pop();

						double firstNumber = 0;
						if (numbers.Count > 0)
							firstNumber = numbers.Pop();

						numbers.Push(ExecuteOperation(expressionValue.Value, firstNumber, secondNumber));
						break;
					case ExpressionValueType.Number:

						numbers.Push(double.Parse(expressionValue.Value.Replace(',','.'), CultureInfo.InvariantCulture));
						break;
					//case ExpressionValueType.Function:

					//	string functionName = Regex.Match(expressionValue.Value, @"^[a-zA-Z]+").Value;
					//	if (!_functions.Functions.ContainsKey(functionName))
					//		throw new Exception("Function doesn't exist");

					//	string functionValue = expressionValue.Value.ToString();
					//	functionValue = Regex.Replace(functionValue, @"^[a-zA-Z]+", "");

					//	foreach (var variable in _variables.Variables)
					//	{
					//		if (functionValue.Contains(variable.Key))
					//		{
					//			string variableValue = variable.Value.ToString();
					//			variableValue = variableValue.Replace(",", ".");
					//			functionValue = functionValue.Replace(variable.Key, variableValue);
					//		}
					//	}

					//	functionValue = functionName + functionValue;

					//	string functionExpression = _functions.GetFunctionExpressionWithArgs(functionValue);
					//	PostfixExpression functionPostfixExpression = new PostfixExpression(functionExpression);
					//	double functionResult = CalculatePostfix(functionPostfixExpression);
					//	numbers.Push(functionResult);
					//	break;
				}
			}

			if (numbers.Count == 1)
			{
				double result = numbers.Pop();
				result = Math.Round(result, 10);
				return result;
			}
			else
				throw new Exception("Incorrect calculation");
		}
		//public void AddVariable(string variable)
		//{
		//	_variables.Calculate(variable);
		//}
		//public void AddFunctions(string function)
		//{
		//	_functions.ParseFunction(function);
		//}
	}
}
