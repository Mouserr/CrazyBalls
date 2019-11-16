using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Tween;
using UnityEngine;

namespace Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations.Tween
{
    [Obsolete("Use MoveTween object")]
    public class TweenMoveToScenarioItem : TweenScenarioItem
    {
        #region Class fields
        private readonly List<GameObject> targets;
        private readonly Vector3 destination;
        private readonly float duration;
        private readonly EaseType ease;
        private readonly TweenSpace tweenSpace;
        #endregion
    
        #region Constructor
        public TweenMoveToScenarioItem(GameObject target, Vector3 destination, float duration, EaseType ease, TweenSpace tweenSpace)
            : this(new List<GameObject>() { target }, destination, duration, ease, tweenSpace, true)
        {
        }

        public TweenMoveToScenarioItem(GameObject target, Vector3 destination, float duration, EaseType ease, TweenSpace tweenSpace, bool isInterruptable)
            : this(new List<GameObject>() { target }, destination, duration, ease, tweenSpace, isInterruptable)
        {
        }

        public TweenMoveToScenarioItem(List<GameObject> targets, Vector3 destination, float duration, EaseType ease, TweenSpace tweenSpace)
            : this(targets, destination, duration, ease, tweenSpace, true)
        {
        }

        public TweenMoveToScenarioItem(List<GameObject> targets, Vector3 destination, float duration, EaseType ease, TweenSpace tweenSpace, bool isInterruptable)
            : base(isInterruptable)
        {
            this.targets = targets;
            this.destination = destination;
            this.duration = duration;
            this.ease = ease;
            this.tweenSpace = tweenSpace;
        }
        #endregion


        #region Override methods

        protected override SimulateScenarioItem CreateTweenObject()
        {
            return TweenPerformer.Instance.MoveTo(targets, destination, duration, ease, tweenSpace);
        }
        #endregion
   
    }
}
