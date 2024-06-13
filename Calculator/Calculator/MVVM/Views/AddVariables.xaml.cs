using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Calculator.MVVM.Views
{
    /// <summary>
    /// Логика взаимодействия для AddVariables.xaml
    /// </summary>
    public partial class AddVariables : Window
    {
        public AddVariables()
        {
            InitializeComponent();
        }
        private Dictionary<string, double> variables = new Dictionary<string, double>();

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            string input = InputTextBox.Text;
            string[] lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                foreach (string line in lines)
                {
                    if (line.Contains("="))
                    {
                        ParseVariable(line);
                    }
                    else
                    {
                        double result = EvaluateExpression(line);
                        ResultTextBlock.Text = result.ToString();
                    }
                }
                UpdateVariablesListBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
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
        private void UpdateVariablesListBox()
        {
            VariablesListBox.Items.Clear();
            foreach (var variable in variables)
            {
                VariablesListBox.Items.Add($"{variable.Key} = {variable.Value}");
            }
        }
    }
}
