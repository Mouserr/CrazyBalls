using Assets.Scripts.Core.SyncCodes.SyncScenario;
using Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations;
using Assets.Scripts.Core.Tween;
using Assets.Scripts.Core.Tween.TweenObjects;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    public abstract class AbstractScreen : MonoBehaviour
    {
        public Vector3 HideEndPosition;
        public Vector3 ShowStartPosition;
        public ScreenType ScreenType;
        public bool IsPopup;

        public virtual ISyncScenarioItem GetHideTransition(float duration = 0.3f)
        {
            return new MoveTween(this, HideEndPosition, duration, EaseType.Linear, TweenSpace.Local);
        }

        public virtual ISyncScenarioItem GetShowTransition(float duration = 0.3f)
        {
            return new SyncScenario(new ActionScenarioItem(PrepareToShow),
                new MoveTween(this, Vector3.zero, duration, EaseType.Linear, TweenSpace.Local));
        }

        protected virtual void PrepareToShow()
        {
            transform.localPosition = ShowStartPosition;
            gameObject.SetActive(true);
        }

        public virtual void Focus()
        {
        }

        public virtual void UnFocus()
        {
            gameObject.SetActive(false);
        }
    }
}