namespace Calculator.MVVM.Models
{
    public enum ExpressionValueType
	{
		Variable,
		Operator,
		Number,
		Bracket
	}
	public class ExpressionValue
	{
		public string Value { get; private set; }
		public ExpressionValueType ValueType { get; private set; }

		public ExpressionValue(string value, ExpressionValueType valueType)
		{
			Value = value;
			ValueType = valueType;
		}
		public void ToUnaryMinus()
		{
			if (ValueType == ExpressionValueType.Operator && Value == "-")
				Value = "~";
		}
	}
}
