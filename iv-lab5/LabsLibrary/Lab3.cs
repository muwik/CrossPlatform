using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LabsLibrary
{
	public class Lab3Runner
	{
		public Lab3Runner(string firstLine, string secondLine, string thirdLine, string fourthLine, string fifthLine, string sixthLine, string seventhLine, string eighthLine)
		{
			_firstLine = firstLine;
			_secondLine = secondLine;
			_thirdLine = thirdLine;
			_fourthLine = fourthLine;
			_fifthLine = fifthLine;
			_sixthLine = sixthLine;
			_seventhLine = seventhLine;
			_eighthLine = eighthLine;
		}

		private readonly string _firstLine;
		private readonly string _secondLine;
		private readonly string _thirdLine;
		private readonly string _fourthLine;
		private readonly string _fifthLine;
		private readonly string _sixthLine;
		private readonly string _seventhLine;
		private readonly string _eighthLine;

		public string RunLab()
		{
			var inputData = new List<string>() { _firstLine, _secondLine, _thirdLine, _fourthLine, _fifthLine, _sixthLine, _seventhLine, _eighthLine };
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