using System.Collections.Generic;
using Assets.Scripts.Core.Curves;
using Assets.Scripts.Core.SyncCodes;
using Assets.Scripts.Core.SyncCodes.SyncScenario;
using Assets.Scripts.Core.Tween;
using Assets.Scripts.Core.Tween.TweenObjects;
using UnityEngine;

namespace Assets.Scripts
{
	public class CircleDamageEffect : MonoBehaviour
	{
		[SerializeField] private float time;
		private ISyncScenarioItem currentItem;

#if UNITY_EDITOR
	    private void Update()
	    {
	        if (currentItem.IsNullOrComplete() && Input.GetKeyUp(KeyCode.Space))
	        {
                Debug.Log("Test");
	            currentItem = GetExplosionItem(time).PlayRegisterAndReturnSelf();
	        }
	    }
#endif

        public ISyncScenarioItem GetExplosionItem(float duration)
		{
			List<ISyncScenarioItem> items = new List<ISyncScenarioItem>();


            items.Add(new AlphaTween(gameObject, 1, duration / 2f, EaseType.QuadIn));
			items.Add(new AlphaTween(gameObject, 0, duration / 2f, EaseType.QuadOut));

			return new SyncScenario(
				items,
				(scenario, interrupted) => new AlphaTween(gameObject, 0).Play()
			);
		}

	    public ISyncScenarioItem Prepare()
	    {
            return new AlphaTween(gameObject, 0);
	    }
	}
}