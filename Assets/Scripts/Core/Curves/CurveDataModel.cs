using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core.Curves
{
	public enum CurveType
	{
		Bezier,
		LinearInterpol,

	}

	[Serializable]
	public class CurveDataModel
	{
		public const string ConfigType = "CurveDataModel";
		public CurveType CurveType;
		public List<Vector3> ControlPoints;

		public CurveDataModel()
		{
			ControlPoints = new List<Vector3>();
		}

		public CurveDataModel(List<Vector3> controlPoints, CurveType curveType)
		{
			ControlPoints = controlPoints;
			CurveType = curveType;
		}
	}
}