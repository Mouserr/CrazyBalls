using UnityEngine;

namespace Assets.Scripts.Core.Curves
{
	public abstract class AbstractCurve
	{
		protected readonly CurveDataModel model;
		public float Length { get; protected set; }

		protected AbstractCurve(CurveDataModel model)
		{
			this.model = model;
		}

		public abstract Vector3 GetValue(float time);
	}
}