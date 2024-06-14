using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.MVVM.Models
{
	public class PostfixCalculator
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
					case ExpressionValueType.Variable:
						throw new NotImplementedException();
						break;
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
						numbers.Push(double.Parse(expressionValue.Value));
						break;
				}
			}

			if (numbers.Count > 0)
				return numbers.Pop();
			else
				throw new Exception("Incorrect calculation");
		}
	}
}
