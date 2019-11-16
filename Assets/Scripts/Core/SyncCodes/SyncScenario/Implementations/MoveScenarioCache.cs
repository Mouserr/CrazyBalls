using System.Collections.Generic;
using Assets.Scripts.Core.Tween;
using Assets.Scripts.Core.Tween.TweenObjects;
using Assets.Scripts.Core.Tween.TweenSimulators;
using UnityEngine;

namespace Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations
{
    public class MoveScenarioCache
    {
        #region Class fields
        private List<float> durations;
        private List<Vector3> positions;
        private List<ISyncScenarioItem> tweens;
        #endregion

        #region Constructor
        public MoveScenarioCache()
        {
            durations = new List<float>();
            positions = new List<Vector3>();
            tweens = new List<ISyncScenarioItem>();
        }
        #endregion

        #region Methods 
        public void AddValue(MoveTween tween)
        {
            positions.Add(tween.EndValue);
            durations.Add(tween.Duration);
            tweens.Add(tween);
        }

        public void AddValue(Vector3 endPosition, float duration, EaseType easing, ISyncScenarioItem tween)
        {
            positions.Add(endPosition);
            durations.Add(duration);
            tweens.Add(tween);
        }

        public Vector3 GetTrackStartPoint(Vector3 pathStartPoint, float trackingBackTime, float rollbackDelaySec)
        {
            Vector3 position = new Vector3(pathStartPoint.x, pathStartPoint.y);
            float leftTime = trackingBackTime + rollbackDelaySec;
            //Debug.LogWarning("camera time  " + leftTime);
            for (int i = 0; i < tweens.Count; i++)
            {
                float tweenLeftTime = durations[i] - this.getCurTime(tweens[i]);
                if (tweenLeftTime > 0)
                {
                    //Debug.LogWarning("not completed tween " + trackingPath[i].GetType() + " number " + i);
                    //Debug.LogWarning("left tween time " + tweenLeftTime);
                    if (leftTime - tweenLeftTime >= 0f)
                    {
                        position = addTweenAction(i, position, null);
                        leftTime -= tweenLeftTime;
                    }
                    else // Go to this tween path
                    {
                        position = addTweenAction(i, position, this.getCurTime(tweens[i]) + leftTime);
                        break;
                    }
                }
                else
                {
                    position = addTweenAction(i, position, null);
                }
            }
            //Debug.LogWarning("Hero will be on " + position + " in " + leftTime + " sec");
            return position;
        }
 
        private float getCurTime(ISyncScenarioItem item)
        { 
            float curTime = 0.0f;

            if(item is MoveTween)
            {
                MoveTween tSh = (item as MoveTween);
                if (tSh != null)
                {
                    curTime = tSh.CurTime;   
                }
            }

            if(item is AlphaTween)
            {
                AlphaTween tF = (item as AlphaTween);

                if(tF != null)
                {
                    curTime = tF.CurTime;   
                }
            }

            if (item is ScaleTween)
            {
                ScaleTween tF = (item as ScaleTween);

                if (tF != null)
                {
                    curTime = tF.CurTime;
                }
            }

            return curTime;
        }

        private Vector3 addTweenAction(int i, Vector3 position, float? leftTime)
        {
            if (tweens[i] is MoveTween)
            {
                MoveTween shiftTween = tweens[i] as MoveTween;
                Vector3TweenSimulator tweenSimulator = shiftTween.Simulator as Vector3TweenSimulator;
                //MoveTween.sim
                if (!leftTime.HasValue)
                {
                    position += shiftTween.EndValue;
                }
                else
                {
                    //Vector3TweenSimulator simulator = new Vector3TweenSimulator(shiftTween.;
                    if (tweenSimulator != null)
                        position += new Vector3(
                            tweenSimulator.SimulateFunction.Invoke(leftTime.Value, 0, shiftTween.EndValue.x, shiftTween.Duration),
                            tweenSimulator.SimulateFunction.Invoke(leftTime.Value, 0, shiftTween.EndValue.y, shiftTween.Duration),
                            tweenSimulator.SimulateFunction.Invoke(leftTime.Value, 0, shiftTween.EndValue.z, shiftTween.Duration)
                            );
                }
            }
            return position;
        }
        #endregion
    }
}
