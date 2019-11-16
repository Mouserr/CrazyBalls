using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Tween;
using UnityEngine;

namespace Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations.Tween
{
    [Obsolete("Use ScaleTween object")]
    public class TweenScaleToScenarioItem : TweenScenarioItem
    {
        #region Class fields
        private readonly List<GameObject> targets;
        private readonly Vector3 endScale;
        private readonly float duration;
        private readonly EaseType ease;
        #endregion

        #region Constructor
        public TweenScaleToScenarioItem(GameObject target, Vector3 endScale, float duration, EaseType ease)
            : this(new List<GameObject>() { target }, endScale, duration, ease, true)
        {
        }

        public TweenScaleToScenarioItem(GameObject target, Vector3 endScale, float duration, EaseType ease, bool isInterruptable)
            : this(new List<GameObject>() { target }, endScale, duration, ease, isInterruptable)
        {   
        }

        public TweenScaleToScenarioItem(List<GameObject> targets, Vector3 endScale, float duration, EaseType ease)
            : this(targets, endScale, duration, ease, true)
        {
        }

        public TweenScaleToScenarioItem(List<GameObject> targets, Vector3 endScale, float duration, EaseType ease, bool isInterruptable)
            : base(isInterruptable)
        {
            this.targets = targets;
            this.endScale = endScale;
            this.duration = duration;
            this.ease = ease;
        }
        #endregion


        #region Override methods
        protected override SimulateScenarioItem CreateTweenObject()
        {
            return TweenPerformer.Instance.ScaleTo(targets, endScale, duration, ease);
        }
        #endregion
    }
}
