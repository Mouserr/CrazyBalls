using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core.Curves
{
	public class LinearCurve : AbstractCurve
	{
		public LinearCurve(List<Vector3> controlPoints)
			: this(new CurveDataModel(controlPoints, CurveType.LinearInterpol))
		{
		}

		public LinearCurve(CurveDataModel model)
			: base(model)
		{
			Length = 0;
			for (int i = 1; i < model.ControlPoints.Count; i++)
			{
				Length += (model.ControlPoints[i] - model.ControlPoints[i - 1]).magnitude;
			}
		}

		public override Vector3 GetValue(float time)
		{
			if (model.ControlPoints.Count == 0) return Vector3.zero;

			Vector3 result = model.ControlPoints[0];
			float curLength = time * Length;
			for (int i = 1; i < model.ControlPoints.Count; i++)
			{
				Vector3 curVector = (model.ControlPoints[i] - model.ControlPoints[i - 1]);
				float magnitude = curVector.magnitude;
				if (curLength > magnitude)
				{
					result = model.ControlPoints[i];
					curLength -= magnitude;
				}
				else
				{
					result += curVector.normalized*curLength;
					break;
				}
			}

			return result;
		}
	}
}