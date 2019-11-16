using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Tween;
using UnityEngine;

namespace Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations.Tween
{
    [Obsolete("Use MoveTween object")]
    public class TweenShiftScenarioItem : TweenScenarioItem
    {
        #region Class fields
        private readonly List<GameObject> targets;
        private readonly Vector3 destination;
        private readonly float duration;
        private readonly EaseType ease;
        private readonly TweenSpace tweenSpace;
        #endregion

        #region Constructor
        public TweenShiftScenarioItem(GameObject target, Vector3 destination, float duration, EaseType ease, TweenSpace tweenSpace)
            : this(new List<GameObject>() { target }, destination, duration, ease, tweenSpace, true)
        {
        }

        public TweenShiftScenarioItem(GameObject target, Vector3 destination, float duration, EaseType ease, TweenSpace tweenSpace, bool isInterruptable)
            : this(new List<GameObject>() { target }, destination, duration, ease, tweenSpace, isInterruptable)
        {   
        }

        public TweenShiftScenarioItem(List<GameObject> targets, Vector3 destination, float duration, EaseType ease, TweenSpace tweenSpace)
            : this(targets, destination, duration, ease, tweenSpace, true)
        {
        }

        public TweenShiftScenarioItem(List<GameObject> targets, Vector3 destination, float duration, EaseType ease, TweenSpace tweenSpace, bool isInterruptable)
            : base(isInterruptable)
        {
            this.targets = targets;
            this.destination = destination;
            this.duration = duration;
            this.ease = ease;
            this.tweenSpace = tweenSpace;
        }
        #endregion

        #region Propertirs
        public Vector3 Destination
        {
            get { return destination; }
        }

        public float Duration
        {
            get { return duration; }
        }

        public EaseFunction Ease
        {
            get { return TweenPerformer.Ease[ease]; }
        }

        #endregion

        #region Override methods
        protected override SimulateScenarioItem CreateTweenObject()
        {
            return TweenPerformer.Instance.ShiftBy(targets, Destination, Duration, ease, tweenSpace);
        }
        #endregion
    }
}