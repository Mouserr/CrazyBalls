using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.UI;
using UnityEngine;

namespace DefaultNamespace
{
    public class HealthBar : ProgressBar
    {
        [SerializeField] private Color _normalColor;
        [SerializeField] private Color _negativeColor;
        [SerializeField] private Color _positiveColor;
        [SerializeField] private Color _mixedColor;
        
        public void BuffUpdated(List<CharacterEffect> effects)
        {
            bool hasPositive = false;
            bool hasNegative = false;
            foreach (var characterEffect in effects)
            {
                if (characterEffect.Config.Positive)
                {
                    hasPositive = true;
                }
                else
                {
                    hasNegative = true;
                }
            }

            if (hasPositive && hasNegative)
            {
                _foreground.color = _mixedColor;
                return;
            }

            if (hasNegative)
            {
                _foreground.color = _negativeColor;
                return;
            }

            if (hasPositive)
            {
                _foreground.color = _positiveColor;
                return;
            }

            _foreground.color = _normalColor;
        }
    }
}