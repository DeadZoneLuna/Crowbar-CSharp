namespace Crowbar
{
	public class IntermediateExpression
	{
		public string theExpression;
		public int thePrecedence;

		public IntermediateExpression(string iExpression, int iPrecedence)
		{
			theExpression = iExpression;
			thePrecedence = iPrecedence;
		}
	}
}