using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core.Curves
{
	public class CurveVisualizer : MonoBehaviour
	{
		public CurveType CurveType;
		public int segmentsCount = 50;
		public bool globalSpace;

		public Vector3[] ControlPoints;
		
		private AbstractCurve curve;

		public void OnDrawGizmosSelected()
		{
			if (ControlPoints == null) return;
			curve = CurveManager.GetCurve(GetCurveModel(false));
			
			Gizmos.color = Color.green;
			Vector3 lastValue = globalSpace ? curve.GetValue(0) : transform.TransformPoint(curve.GetValue(0));
			for (int i = 1; i < segmentsCount; i++)
			{
				//float t = (i - 1f) / (segmentsCount - 1f);
				float t1 = i / (segmentsCount - 1f);
				Vector3 newValue = globalSpace ? curve.GetValue(t1) : transform.TransformPoint(curve.GetValue(t1));
				Gizmos.DrawLine(lastValue, newValue);
				lastValue = newValue;
			}
		}

		public CurveDataModel GetCurveModel(bool withGlobal)
		{
			if (withGlobal && !globalSpace)
			{
				List<Vector3> controlPoints = new List<Vector3>(ControlPoints);
				for (int i = 0; i < controlPoints.Count; i++)
				{
					controlPoints[i] = transform.TransformPoint(controlPoints[i]);
				}

				return new CurveDataModel(controlPoints, CurveType);
			}
			
			return ControlPoints != null
				? new CurveDataModel(new List<Vector3>(ControlPoints), CurveType) 
				: null;
		}

		public void ReversePoints()
		{
			if (ControlPoints != null)
			{
				List<Vector3> list = new List<Vector3>(ControlPoints);
				list.Reverse();
				ControlPoints = list.ToArray();
			}
		}

		public void ShiftBy(Vector3 shift)
		{
			for (int i = 0; i < ControlPoints.Length; i++)
			{
				ControlPoints[i] += shift;
			}
		}

		public void ToGlobal()
		{
			if (globalSpace) return;

			for (int i = 0; i < ControlPoints.Length; i++)
			{
				ControlPoints[i] = transform.TransformPoint(ControlPoints[i]);
			}

			globalSpace = true;
		}

		public void ToLocal()
		{
			if (!globalSpace) return;

			for (int i = 0; i < ControlPoints.Length; i++)
			{
				ControlPoints[i] = transform.InverseTransformPoint(ControlPoints[i]);
			}

			globalSpace = false;
		}
	}
}