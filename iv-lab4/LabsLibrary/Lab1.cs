using System.Data;

namespace LabsLibrary
{
	public static class Lab1
	{
		public static string RunLab(string pathInpFile = "INPUT.TXT")
		{
			var numArr = File.ReadLines(pathInpFile).First().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToList();

			if (numArr.Any(num => num < -104 || num > 104))
			{
				return "Out of range exception!";
			}
			else
			{
				return F(numArr[0], numArr[1], numArr[2]).ToString();
			}
		}
		private static Dictionary<Tuple<long, long, long>, long> buf = new Dictionary<Tuple<long, long, long>, long>();
		public static long F(long a, long b, long c)
		{
			if (a <= 0 || b <= 0 || c <= 0)
				return 1;
			else if (a > 20 || b > 20 || c > 20)
			{
				var tpl = new Tuple<long, long, long>(20, 20, 20);
				if (!buf.ContainsKey(tpl))
				{
					buf.Add(tpl, F(20, 20, 20));
				}
				return buf[tpl];
			}
			else if (a < b && b < c)
			{
				var tpl1 = new Tuple<long, long, long>(a, b - 1, c);
				var tpl2 = new Tuple<long, long, long>(a, b, c - 1);
				var tpl3 = new Tuple<long, long, long>(a, b - 1, c - 1);
				if (!buf.ContainsKey(tpl1)) { buf.Add(tpl1, F(a, b - 1, c)); }
				if (!buf.ContainsKey(tpl2)) { buf.Add(tpl2, F(a, b, c - 1)); }
				if (!buf.ContainsKey(tpl3)) { buf.Add(tpl3, F(a, b - 1, c - 1)); }
				return buf[tpl1] + buf[tpl2] - buf[tpl3];
			}
			else
			{
				a--;
				var tpl1 = new Tuple<long, long, long>(a, b - 1, c);
				var tpl2 = new Tuple<long, long, long>(a, b, c - 1);
				var tpl3 = new Tuple<long, long, long>(a, b - 1, c - 1);
				var tpl4 = new Tuple<long, long, long>(a, b, c);
				if (!buf.ContainsKey(tpl1)) { buf.Add(tpl1, F(a, b - 1, c)); }
				if (!buf.ContainsKey(tpl2)) { buf.Add(tpl2, F(a, b, c - 1)); }
				if (!buf.ContainsKey(tpl3)) { buf.Add(tpl3, F(a, b - 1, c - 1)); }
				if (!buf.ContainsKey(tpl4)) { buf.Add(tpl4, F(a, b, c)); }
				return buf[tpl4] + buf[tpl1] + buf[tpl2] - buf[tpl3];
			}
		}
	}
}