using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Screens
{
    public class UICharacterAbility : MonoBehaviour
    {
        private UnitController _unit;
        private int _hitsToUnlock;

        [SerializeField]
        private Image Icon;

        [SerializeField]
        private TMP_Text HitsLeftLabel;

        public void Init(UnitController unit)
        {
            _unit = unit;
            Icon.sprite = unit.Character.Icon;
            _hitsToUnlock = unit.Character.ActiveAbility.HitsToUnlock;
            unit.HitsCount.Changed += OnHit;
            HitsLeftLabel.text = _hitsToUnlock.ToString();
        }

        private void OnHit(CharacterStat characterStat)
        {
            var hitsLeft = _hitsToUnlock - _unit.HitsCount.CurrentValue;
            HitsLeftLabel.text = hitsLeft > 0 ? hitsLeft.ToString() : "Ready";
        }

        public void Click()
        {
            if (Game.Instance.CurrentUnit == _unit && _unit.HitsCount.CurrentValue >= _hitsToUnlock)
            {
                _unit.CastAbility(new CastContext {Caster = _unit});
            }
        }
    }
}