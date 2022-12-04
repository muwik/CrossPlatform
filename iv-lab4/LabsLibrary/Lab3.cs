using System.Text.RegularExpressions;

namespace LabsLibrary
{
	public static class Lab3
	{
		public static string RunLab(string pathInpFile = "INPUT.TXT")
		{
			var inputData = File.ReadLines(pathInpFile).ToList();
			var borderLines = GetBorderLines(inputData);
			var amountOfBuilders = CountAmountOfBuilders(borderLines);

			return amountOfBuilders.ToString();
		}

		private static List<string> GetBorderLines(List<string> inputData)
		{
			var topBorderLine = inputData.First();
			var bottomBorderLine = inputData.Last();
			var leftBorderLine = string.Empty;
			var rightBorderLine = string.Empty;

			foreach (var plateLine in inputData)
			{
				leftBorderLine += plateLine.First();
				rightBorderLine += plateLine.Last();
			}

			var resultBorderLines = new List<string>() { topBorderLine, bottomBorderLine, leftBorderLine, rightBorderLine };
			return resultBorderLines;
		}

		private static int CountAmountOfBuilders(List<string> borderLines)
		{
			var amountOfBuilders = 0;
			foreach (var borderLine in borderLines)
			{
				var wwAmount = Regex.Matches(borderLine, "WW").Count;
				var bbAmount = Regex.Matches(borderLine, "BB").Count;
				amountOfBuilders += wwAmount + bbAmount;
			}

			return amountOfBuilders;
		}
	}
}