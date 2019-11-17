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

        public virtual ISyncScenarioItem GetHideTransition()
        {
            return new MoveTween(this, HideEndPosition, 0.3f, EaseType.Linear, TweenSpace.Local);
        }

        public virtual ISyncScenarioItem GetShowTransition()
        {
            return new SyncScenario(new ActionScenarioItem(PrepareToShow),
                new MoveTween(this, Vector3.zero, 0.3f, EaseType.Linear, TweenSpace.Local));
        }

        protected virtual void PrepareToShow()
        {
            transform.localPosition = ShowStartPosition;
            gameObject.SetActive(true);
        }

        [ContextMenu("Set Show Start")]
        public void SetShowStart()
        {
            ShowStartPosition = transform.localPosition;
        }
        
        [ContextMenu("Set Hide End")]
        public void SetHideEnd()
        {
            HideEndPosition = transform.localPosition;
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