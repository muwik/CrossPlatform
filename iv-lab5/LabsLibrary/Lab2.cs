using System;
using System.Collections.Generic;

namespace LabsLibrary
{
	public class Lab2Runner
	{
		public Lab2Runner(string inputNumber)
		{
			_inputNumber = inputNumber;
		}

		private readonly string _inputNumber;

		public string RunLab()
		{
			var inputNumber = Convert.ToInt32(_inputNumber);

			if (inputNumber < 1 || inputNumber > 106)
			{
				return "Out of range exception!";
			}
			else
			{
				return GetMinCountToOne(inputNumber).ToString();
			}
		}

		private static int GetMinCountToOne(int inputNumber)
		{
			List<int> buf = new List<int>() { 0, 1, 1 };
			for (int i = 3; i < inputNumber; i++)
			{
				int s = i + 1;
				if (s % 2 == 0 && s % 3 == 0)
				{
					buf.Add(Math.Min(buf[i - 1], Math.Min(buf[i / 2], buf[i / 3])) + 1);
				}
				else if (s % 2 == 0)
				{
					buf.Add(Math.Min(buf[i - 1], buf[i / 2]) + 1);
				}
				else if (s % 3 == 0)
				{
					buf.Add(Math.Min(buf[i - 1], buf[i / 3]) + 1);
				}
				else
				{
					buf.Add(buf[i - 1] + 1);
				}
			}
			return buf[inputNumber - 1];
		}
	}
}