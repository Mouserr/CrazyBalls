namespace Assets.Scripts.Core.Curves
{
	public class CurveManager
	{
		private static CurveManager instance;

		public static CurveManager Instance
		{
			get { return instance ?? (instance = new CurveManager()); }
		}


        private CurveManager()
		{
		}

		public static AbstractCurve GetCurve(CurveDataModel model)
		{
			if (model == null) return null;
			switch (model.CurveType)
			{
				case CurveType.Bezier:
					return new BezierCurve(model);
				case CurveType.LinearInterpol:
					return new LinearCurve(model);
			}

			return null;
		}
	}
}