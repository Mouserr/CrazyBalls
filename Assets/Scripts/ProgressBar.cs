using Assets.Scripts.Core.SyncCodes.SyncScenario;
using Assets.Scripts.Core.Tween;
using Assets.Scripts.Core.Tween.TweenObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ProgressBar : MonoBehaviour
    {
        private Vector2 _backgroundSize;
        private float _currentValue;
        private ISyncScenarioItem _scenario;

        [SerializeField]
        protected Image _foreground;
        [SerializeField]
        private Image _backgound;

        private void Awake()
        {
            _backgroundSize = _backgound.rectTransform.sizeDelta;
            UpdateView();
        }

        public void SetValue(CharacterStat stat)
        {
            SetValue(stat.Progress);
        }

        public void SetValue(float value, bool instant = false)
        {
            _currentValue = Mathf.Clamp01(value);
            if (instant)
            {
                UpdateView();
            }
            else
            {
                _scenario?.Stop();
                _scenario = new SizeTween(_foreground.rectTransform, GetEndSize(), 0.3f, EaseType.Linear);
                _scenario.Play();
            }
        }

        private void UpdateView()
        {
            _foreground.rectTransform.sizeDelta = GetEndSize();
        }

        private Vector2 GetEndSize()
        {
            return new Vector2(_backgroundSize.x * _currentValue, _foreground.rectTransform.sizeDelta.y);
        }
    }
}