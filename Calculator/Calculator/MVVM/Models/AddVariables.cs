using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolee
{
    public class AddVariables
    {
        private Dictionary<string, double> variables = new Dictionary<string, double>();
        public Dictionary<string, double> Variables => new Dictionary<string, double>(variables);

        public double Calculate(string input)
        {
            string[] lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            double result = 0;
            foreach (string line in lines)
            {
                if (line.Contains("="))
                {
                    ParseVariable(line);
                }
                else
                {
                    result = EvaluateExpression(line);
                }
            }
            return result;
        }

        private void ParseVariable(string line)
        {
            string[] parts = line.Split('=');
            if (parts.Length == 2)
            {
                string variableName = parts[0].Trim();
                double value = EvaluateExpression(parts[1]);
                if (variables.ContainsKey(variableName))
                {
                    variables[variableName] = value;
                }
                else
                {
                    variables.Add(variableName, value);
                }
            }
            else
            {
                throw new Exception("Invalid variable declaration.");
            }
        }

        private double EvaluateExpression(string expression)
        {
            foreach (var variable in variables)
            {
                expression = expression.Replace(variable.Key, variable.Value.ToString());
            }
            return Evaluate(expression);
        }

        private double Evaluate(string expression)
        {
            var dataTable = new System.Data.DataTable();
            return Convert.ToDouble(dataTable.Compute(expression, string.Empty));
        }
    }
}
