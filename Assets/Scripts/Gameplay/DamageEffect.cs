﻿using System.Collections.Generic;
using Assets.Scripts.Core.Curves;
using Assets.Scripts.Core.SyncCodes;
using Assets.Scripts.Core.SyncCodes.SyncScenario;
using Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations;
using Assets.Scripts.Core.Tween;
using Assets.Scripts.Core.Tween.TweenObjects;
using UnityEngine;

namespace Assets.Scripts
{
	public class DamageEffect : MonoBehaviour
	{
		[SerializeField] private CurveVisualizer curveVisualizer;
		[SerializeField] private float time;
		[SerializeField] private CircleDamageEffect circle;
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
				 currentItem = GetExplosionItem(time).PlayRegisterAndReturnSelf();
			}
		}
#endif

		public ISyncScenarioItem GetExplosionItem(float duration)
		{
			List<ISyncScenarioItem> items = new List<ISyncScenarioItem>();

			var moveCurve = CurveManager.GetCurve(curveVisualizer.GetCurveModel(true));
		    items.Add(
		        new CompositeItem(
		            new MoveByCurveTween(moveCurve, gameObject, duration, EaseType.Linear),
		            circle.GetExplosionItem(0.1f)
		        ));


            return new SyncScenario(
				items,
				(scenario, interrupted) =>
				{
                    circle.Prepare().Play();
                    new MoveTween(gameObject, Vector3.zero, TweenSpace.Local).Play();
				});
		}
	}
}