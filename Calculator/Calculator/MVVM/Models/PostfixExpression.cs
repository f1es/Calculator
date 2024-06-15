using System.Text;
using System.Text.RegularExpressions;

namespace Calculator.MVVM.Models
{
    public class PostfixExpression
	{
		private string _infixExpression;
		private Queue<ExpressionValue> _expression;
		private List<string> _operators = new List<string>()
		{
			"+",
			"-",
			"*",
			"/",
			"^",
		};
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
		public string InfixExpression
		{
			get => _infixExpression;
			private set => _infixExpression = value;
		}
		public Queue<ExpressionValue> Expression
		{
			get => _expression;
			private set => value = _expression;
		}
		public PostfixExpression(string expression)
		{
			_infixExpression = expression;
			var separatedExpression = SeparateExpression(expression);
            Validate(separatedExpression);
            ToPostfix(separatedExpression);
		}
		public static PostfixExpression Parse(string expression)
		{
			PostfixExpression postfixExpression = new PostfixExpression(expression);
			return postfixExpression;
		}
		private Queue<ExpressionValue> SeparateExpression(string expression)
		{
			if (expression == null)
				throw new ArgumentNullException(nameof(expression));

			Queue<ExpressionValue> separatedExpression = new Queue<ExpressionValue>();
			StringBuilder value = new StringBuilder();

			bool isFunction = false;
			foreach (var symbol in expression)
			{
				char space = ' ';
				if (symbol == space)
					continue;

				char openBracket = '(';
				if (symbol == openBracket && value.Length > 0)
				{
					value.Append(symbol);
					isFunction = true;
					continue;
				}

				char closeBracket = ')';
				if (symbol == closeBracket && isFunction)
				{
					value.Append(symbol);
					isFunction = false;
					continue;
				}

				if (symbol == openBracket || symbol == closeBracket)
				{
					if (value.Length > 0)
					{
						separatedExpression.Enqueue(IdentifyValue(value.ToString()));
						value.Clear();
					}
					separatedExpression.Enqueue(new ExpressionValue(symbol.ToString(), ExpressionValueType.Bracket));
					continue;
				}

				if (_operators.Contains(symbol.ToString()))
				{
					if (value.Length > 0)
					{
						separatedExpression.Enqueue(IdentifyValue(value.ToString()));
						value.Clear();
					}
					separatedExpression.Enqueue(new ExpressionValue(symbol.ToString(), ExpressionValueType.Operator));
				}
				else
				{
					value.Append(symbol);
				}
			}

			if (value.Length > 0)
				separatedExpression.Enqueue(IdentifyValue(value.ToString()));

			return separatedExpression;
		}

		private ExpressionValue IdentifyValue(string value)
		{

			if (Regex.IsMatch(value.ToString(), @"^[a-zA-Z]+[(][a-zA-Z0-9,]{0,}[][)]"))
			{
				return new ExpressionValue(value.ToString(), ExpressionValueType.Function);
			}
			else if (Regex.IsMatch(value.ToString(), @"^[a-zA-Z]+"))
			{
				return new ExpressionValue(value.ToString(), ExpressionValueType.Variable);
			}
			else if (double.TryParse(value.ToString(), out _))
			{
				return new ExpressionValue(value.ToString(), ExpressionValueType.Number);
			}
			else
				throw new Exception("Incorrect input");
		}
		private void Validate(Queue<ExpressionValue> separatedExpression)
        {
            int bracketCounter = 0;
            ExpressionValue previousExpressionValue = new ExpressionValue("0", ExpressionValueType.Number);
            foreach (var expression in separatedExpression)
            {
                switch (expression.ValueType)
                {
                    case ExpressionValueType.Variable:
                        throw new NotImplementedException();
                        break;
                    case ExpressionValueType.Operator:

                        if (previousExpressionValue.ValueType == ExpressionValueType.Operator)
                            throw new Exception("Operators shouldn't repeat");

                        break;
                    case ExpressionValueType.Number:

                        if (!double.TryParse(expression.Value, out double a))
                            throw new Exception("Incorrect number");

                        break;
                    case ExpressionValueType.Bracket:

                        if (expression.Value == "(")
                            bracketCounter++;
                        else
                            bracketCounter--;

                        break;
                }

                previousExpressionValue = expression;
            }

            if (bracketCounter > 0)
                throw new Exception("All brackets should be closed");
            else if (bracketCounter < 0)
                throw new Exception("Unnecessary bracket");
        }
        private void ToPostfix(Queue<ExpressionValue> normalExpression)
		{
			Queue<ExpressionValue> postfixExpression = new Queue<ExpressionValue>();
			Stack<ExpressionValue> operators = new Stack<ExpressionValue>();
			ExpressionValue previousExpressionValue = new ExpressionValue("0", ExpressionValueType.Number);
			foreach (var expressionValue in normalExpression)
			{
				switch (expressionValue.ValueType)
				{
					case ExpressionValueType.Variable:
						throw new NotImplementedException();
						break;
					case ExpressionValueType.Operator:

						_operatorsPriority.ContainsKey(previousExpressionValue.Value);

						if (expressionValue.Value == "-" && (postfixExpression.Count == 0 || (postfixExpression.Count > 0 && _operatorsPriority.ContainsKey(previousExpressionValue.Value))))
							expressionValue.ToUnaryMinus();

						while (operators.Count > 0 && (_operatorsPriority[operators.Peek().Value] >= _operatorsPriority[expressionValue.Value]))
							postfixExpression.Enqueue(operators.Pop());

						operators.Push(expressionValue);

						break;
					case ExpressionValueType.Number:

						postfixExpression.Enqueue(expressionValue);

						break;
					case ExpressionValueType.Bracket:

						if (expressionValue.Value == "(")
						{
							operators.Push(expressionValue);
						}
						else
						{
							while (operators.Count > 0 && operators.Peek().Value != "(")
								postfixExpression.Enqueue(operators.Pop());
							operators.Pop();
						}
						break;
					default:
						throw new Exception("Unknown expression value");
				}
				previousExpressionValue = expressionValue;
			}

			foreach (var op in operators)
				postfixExpression.Enqueue(op);

			_expression = postfixExpression;
		}
	}
}
