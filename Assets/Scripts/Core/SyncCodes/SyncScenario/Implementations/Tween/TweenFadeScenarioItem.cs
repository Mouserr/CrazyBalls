using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Tween;
using UnityEngine;

namespace Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations.Tween
{
    [Obsolete("Use AlphaTween object")]
    public class TweenFadeScenarioItem : TweenScenarioItem
    {
        #region Class fields
        private readonly List<GameObject> targets;
        private readonly float endOpaque;
        private readonly float duration;
        private readonly EaseType ease;
        #endregion

        #region Constructor
        public TweenFadeScenarioItem(GameObject target, float endOpaque, float duration, EaseType ease)
            : this(new List<GameObject>() { target }, endOpaque, duration, ease, true)
        {
        }

        public TweenFadeScenarioItem(GameObject target, float endOpaque, float duration, EaseType ease, bool isInterruptable)
            : this(new List<GameObject>() { target }, endOpaque, duration, ease, isInterruptable)
        {
        }

        public TweenFadeScenarioItem(List<GameObject> targets, float endOpaque, float duration, EaseType ease)
            : this(targets, endOpaque, duration, ease, true)
        {
        }

        public TweenFadeScenarioItem(List<GameObject> targets, float endOpaque, float duration, EaseType ease, bool isInterruptable)
            : base(isInterruptable)
        {
            this.targets = targets;
            this.endOpaque = endOpaque;
            this.duration = duration;
            this.ease = ease;
        }
        #endregion


        #region Override methods
        protected override SimulateScenarioItem CreateTweenObject()
        {
            return TweenPerformer.Instance.Fade(targets, endOpaque, duration, ease);
        }
        #endregion
    }
}
