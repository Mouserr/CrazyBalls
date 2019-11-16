using System.Collections.Generic;
using Assets.Scripts.Core.Helpers;
using UnityEngine;

namespace Assets.Scripts.Core.Curves
{
	public class BezierCurve : AbstractCurve
	{
		private int lengthCalculationStepsCount = 100;
		private List<float> fixedTimeIntervals = new List<float>();

		public BezierCurve(List<Vector3> controlPoints)
			: this(new CurveDataModel(controlPoints, CurveType.Bezier))
		{
		}

		public BezierCurve(CurveDataModel model)
			: base(model)
		{
			Length = 0;
			Vector3 prevPoint = GetValue(0);
			List<float> lengthParts = new List<float>();
			for (int i = 1; i <= lengthCalculationStepsCount; i++)
			{
				Vector3 curPoint = GetValue(i/(float) lengthCalculationStepsCount);
				lengthParts.Add((curPoint - prevPoint).magnitude);
				Length += lengthParts[i-1];
				prevPoint = curPoint;
			}
			fixedTimeIntervals.Add(0);
			for (int i = 0; i < lengthParts.Count; i++)
			{
				fixedTimeIntervals.Add(fixedTimeIntervals[i] + lengthParts[i] / Length);
			}
		}


		public sealed override Vector3 GetValue(float time)
		{
			time = getFixedTime(time);
			Vector3 result = Vector3.zero;

			for (int i = 0; i < model.ControlPoints.Count; i++)
			{
				result += model.ControlPoints[i] * basisFunction(i, time);
			}
			return result;
		}

		private float getFixedTime(float time)
		{
			for (int i = 1; i < fixedTimeIntervals.Count; i++)
			{
				if (time < fixedTimeIntervals[i])
				{
					float stepTime = (i - 1f) / lengthCalculationStepsCount;
					return stepTime + (float)((time - fixedTimeIntervals[i - 1])/ (double)(fixedTimeIntervals[i] - fixedTimeIntervals[i - 1])) / lengthCalculationStepsCount;
				}
			}

			return time;
		}

		private float basisFunction(int i, float time)
		{
			int n = model.ControlPoints.Count - 1;
			return BinomialCoefficient.С(n, i) * Mathf.Pow(time, i) * Mathf.Pow(1 - time, n - i);
		}
	}
}