using System.Collections.Generic;

namespace Assets.Scripts.Core.Helpers
{
	public class BinomialCoefficient
	{
		private List<long[]> pascalTriangle = new List<long[]>();

		private static BinomialCoefficient instance;
		private static BinomialCoefficient Instance
		{
			get { return instance ?? (instance = new BinomialCoefficient()); }
		}

		/// <summary>
		/// Число сочитаний из n по к
		/// </summary>
		public static long С(int n, int k)
		{
			if (n < 0 || k < 0 || k > n) return -1;
			Instance.constructTriangleIfNeeded(n);

			//Debug.Log(string.Format("pascalTriangle[{0}][{1}] = {2}", n, k, Instance.pascalTriangle[n][k]));
			return Instance.pascalTriangle[n][k];
		}

		private void constructTriangleIfNeeded(int length)
		{
			if (pascalTriangle.Count > length + 1)
			{
				return;
			}

			for (int i = pascalTriangle.Count; i <= length; i++)
			{
				long[] row = new long[i + 1];

				row[0] = row[i] = 1;

				for (int j = 1; j < i; j++)
				{
					row[j] = pascalTriangle[i - 1][j - 1] + pascalTriangle[i - 1][j];
				}

				pascalTriangle.Add(row);
			}
		}
	}
}