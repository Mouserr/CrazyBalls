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
        
        public GameObject CounterUI;
        
        public void Init(UnitController unit)
        {
            _unit = unit;
            Icon.sprite = unit.Character.Icon;
            _hitsToUnlock = unit.Character.ActiveAbility.HitsToUnlock;
            unit.HitsCount.Changed += OnHit;
            CounterUI.transform.Find("UltLabel").GetComponent<TextMeshProUGUI>().text = _hitsToUnlock.ToString();
        }

        private void OnHit(CharacterStat characterStat)
        {
            var hitsLeft = _hitsToUnlock - _unit.HitsCount.CurrentValue;

            if (hitsLeft <= 0)
            {
                CounterUI.SetActive(false);
            }
            else
            {
                CounterUI.SetActive(true);
                CounterUI.transform.Find("UltLabel").GetComponent<TextMeshProUGUI>().text = hitsLeft.ToString();
            }


        }

        public void Click()
        {
            if (Game.Instance.CurrentUnit == _unit && _unit.HitsCount.CurrentValue >= _hitsToUnlock && UIDragController.Instance.IsActive)
            {
                _unit.CastAbility(new CastContext {Caster = _unit});
            }
        }
    }
}