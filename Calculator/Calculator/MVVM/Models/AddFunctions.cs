using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Calculator.MVVM.Models
{
	public class AddFunctions
	{
		private Dictionary<string, (string[] Parameters, string Body, Func<double[], double> Function, string BodyWithHeader)> functions = new Dictionary<string, (string[], string, Func<double[], double>, string)>();

		public Dictionary<string, (string[] Parameters, string Body, Func<double[], double> Function, string BodyWithHeader)> Functions
		{ 
			get => functions; 
			private set => functions = value; 
		}
		public Dictionary<string, string> ViewFunctions
		{
			get
			{
				var funcs = new Dictionary<string,string>();
				foreach (var kvp in functions)
				{
					funcs[kvp.Key] = kvp.Value.BodyWithHeader;
				}
				return funcs;
			}
		}
		public string ParseFunction(string line)
		{
			int equalIndex = line.IndexOf('=');
			string header = line.Substring(0, equalIndex).Trim();
			string body = line.Substring(equalIndex + 1).Trim();

			int openParenIndex = header.IndexOf('(');
			int closeParenIndex = header.IndexOf(')');

			if (openParenIndex == -1 || closeParenIndex == -1 || openParenIndex > closeParenIndex)
			{
				throw new Exception("Invalid function declaration.");
			}

			string functionName = header.Substring(0, openParenIndex).Trim();
			string[] parameters = header.Substring(openParenIndex + 1, closeParenIndex - openParenIndex - 1).Split(',');

			Func<double[], double> function = (args) =>
			{
				if (args.Length != parameters.Length)
				{
					throw new ArgumentException("Incorrect number of arguments provided for the function.");
				}

				var localVariables = new Dictionary<string, double>();

				for (int i = 0; i < parameters.Length; i++)
				{
					localVariables[parameters[i].Trim()] = args[i];
				}

				return EvaluateExpression(body, localVariables);
			};

			functions[functionName] = (parameters, body, function, line);

			return line;
		}

		public double EvaluateFunction(string functionName, double[] args)
		{
			if (functions.ContainsKey(functionName))
			{
				return functions[functionName].Function(args);
			}
			else
			{
				throw new Exception($"Function '{functionName}' is not defined.");
			}
		}

		public string GetFunctionExpressionWithArgs(string functionCall)
		{
			var match = Regex.Match(functionCall, @"(\w+)\(([^)]*)\)");
			if (!match.Success)
			{
				throw new ArgumentException("Invalid function call format.");
			}

			string functionName = match.Groups[1].Value;
			string[] argsStr = match.Groups[2].Value.Split(',');

			if (!functions.ContainsKey(functionName))
			{
				throw new Exception($"Function '{functionName}' is not defined.");
			}

			var (parameters, body, function, line) = functions[functionName];

			if (argsStr.Length != parameters.Length)
			{
				throw new ArgumentException("Incorrect number of arguments provided for the function.");
			}

			double[] args;
			if (argsStr[0] != "")
				args = Array.ConvertAll(argsStr, s => double.Parse(s, CultureInfo.InvariantCulture));
			else
				args = Array.Empty<double>();

			var substitutedExpression = SubstituteArgsInExpression(body, parameters, args);
			return substitutedExpression;
		}

		private string SubstituteArgsInExpression(string expression, string[] parameters, double[] args)
		{
			if (parameters[0] == "")
				return expression;

			for (int i = 0; i < parameters.Length; i++)
			{
				expression = expression.Replace(parameters[i].Trim(), '(' + args[i].ToString() + ')');
			}

			return expression;
		}

		private double EvaluateExpression(string expression, Dictionary<string, double> localVariables)
		{
			foreach (var variable in localVariables)
			{
				expression = expression.Replace(variable.Key, variable.Value.ToString());
			}

			return Evaluate(expression);
		}

		private double Evaluate(string expression)
		{
			var dataTable = new DataTable();
			return Convert.ToDouble(dataTable.Compute(expression, string.Empty));
		}
		public void RemoveFunction(string functionName)
		{
			Functions.Remove(functionName);
		}
	}
}
