using System;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    [CreateAssetMenu(fileName = "NewFoodBonus", menuName = "Bonus: Food")]
    public class FoodBonusData : BonusData
    {
        [SerializeField]
        private int food;

        public int Food => food;
    }
}