using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace Lab2_54
{
    public class Program
    {
        public static string InputFilePath = @"..\..\input.txt";
        public static string OutputFilePath = @"..\..\output.txt";

        static void Main(string[] args)
        {
            FileInfo outputFileInfo = new FileInfo(OutputFilePath);
            var inputNumber = Convert.ToInt32(File.ReadLines(InputFilePath).FirstOrDefault());

            using (StreamWriter streamWriter = outputFileInfo.CreateText())
            {
                if (inputNumber < 1 || inputNumber > 106)
                {
                    streamWriter.WriteLine("Out of range exception!");
                }
                else
                {
                    streamWriter.WriteLine(GetMinCountToOne(inputNumber));
                }
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
                    buf.Add(Math.Min(buf[i - 1], Math.Min(buf[i/2], buf[i/3])) + 1);
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