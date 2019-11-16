using System.Collections.Generic;
using Assets.Scripts.Core.Curves;
using Assets.Scripts.Core.SyncCodes;
using Assets.Scripts.Core.SyncCodes.SyncScenario;
using Assets.Scripts.Core.Tween;
using Assets.Scripts.Core.Tween.TweenObjects;
using UnityEngine;

namespace Assets.Scripts
{
	public class DamageEffect : MonoBehaviour
	{
		[SerializeField] private CurveVisualizer curveVisualizer;
		[SerializeField] private float time;
		private ISyncScenarioItem currentItem;

		[ContextMenu("toZero")]
		public void ShiftToZero()
		{
			curveVisualizer.ShiftBy(-curveVisualizer.ControlPoints[0]);
		}

#if UNITY_EDITOR
		private void Update()
		{
			if (currentItem.IsNullOrComplete() && Input.GetKeyUp(KeyCode.Space))
			{
				 currentItem = GetExplosionItem(time).PlayAndReturnSelf();
			}
		}
#endif

		public ISyncScenarioItem GetExplosionItem(float duration)
		{
			List<ISyncScenarioItem> items = new List<ISyncScenarioItem>();

			items.Add(new MoveByCurveTween(CurveManager.GetCurve(curveVisualizer.GetCurveModel(true)), gameObject, duration, EaseType.Linear));

			return new SyncScenario(
				items
			);
		}
	}
}