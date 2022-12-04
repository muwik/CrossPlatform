using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace iv_lab3
{
	public class Program
	{
		public static string InputFilePath = @"..\..\input.txt";
		public static string OutputFilePath = @"..\..\output.txt";

		static void Main(string[] args)
		{
			var inputData = File.ReadLines(InputFilePath).ToList();
			var borderLines = GetBorderLines(inputData);
			var amountOfBuilders = CountAmountOfBuilders(borderLines);
			System.Console.WriteLine(amountOfBuilders);

			FileInfo outputFileInfo = new FileInfo(OutputFilePath);
			using (StreamWriter streamWriter = outputFileInfo.CreateText())
			{
				streamWriter.WriteLine(amountOfBuilders);
			}
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