using System;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    [CreateAssetMenu(fileName = "NewEnergyBonus", menuName = "Bonus: Energy")]
    public class EnergyBonusData : BonusData
    {
        [SerializeField]
        private int energy;

        public int Energy => energy;
    }
}