using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ProgressBar : MonoBehaviour
    {
        private Vector2 _backgroundSize;
        private float _currentValue;

        [SerializeField]
        private Image _foreground;
        [SerializeField]
        private Image _backgound;

        private void Awake()
        {
            _backgroundSize = _backgound.rectTransform.sizeDelta;
            UpdateView();
        }

        public void SetValue(float value)
        {
            _currentValue = Mathf.Clamp01(value);
            UpdateView();
        }

        private void UpdateView()
        {
            _foreground.rectTransform.sizeDelta = new Vector2(_backgroundSize.x * _currentValue, _foreground.rectTransform.sizeDelta.y);
        }
    }
}