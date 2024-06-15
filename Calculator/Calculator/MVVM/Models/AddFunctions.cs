using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.MVVM.Models
{
    internal class AddFunctions
    {
        private Dictionary<string, Func<double[], double>> functions = new Dictionary<string, Func<double[], double>>();

        public Dictionary<string, Func<double[], double>> Functions => new Dictionary<string, Func<double[], double>>(functions);

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

            functions[functionName] = function;

            return line;
        }

        public double EvaluateFunction(string functionName, double[] args)
        {
            if (functions.ContainsKey(functionName))
            {
                return functions[functionName](args);
            }
            else
            {
                throw new Exception($"Function '{functionName}' is not defined.");
            }
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
    }
}
