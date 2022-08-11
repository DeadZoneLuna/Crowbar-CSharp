namespace Crowbar
{
	public class StringClass
	{
		public static string ConvertFromNullTerminatedOrFullLengthString(string input)
		{
			int positionOfFirstNullChar = input.IndexOf('\0');
			if (positionOfFirstNullChar == -1)
				return input;

			return input.Substring(0, positionOfFirstNullChar); ;
		}

		public static string RemoveUptoAndIncludingFirstDotCharacterFromString(string input)
		{
			int positionOfFirstDotChar = input.IndexOf(".");
			if (positionOfFirstDotChar >= 0)
				return input.Substring(positionOfFirstDotChar + 1);

			return input;
		}
	}
}