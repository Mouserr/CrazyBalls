using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class BonusData : ScriptableObject
    {
    }

    [Serializable]
    [CreateAssetMenu(fileName = "NewCharacterBonus", menuName = "Bonus: Character")]
    public class CharacterBonusData : BonusData
    {
        [SerializeField]
        private AllyCharacterData allyCharacter;

        public AllyCharacterData AllyCharacter => allyCharacter;
    }

    [Serializable]
    [CreateAssetMenu(fileName = "NewEnergyBonus", menuName = "Bonus: Energy")]
    public class EnergyBonusData : BonusData
    {
        [SerializeField]
        private int energy;

        public int Energy => energy;
    }

    [Serializable]
    [CreateAssetMenu(fileName = "NewFoodBonus", menuName = "Bonus: Food")]
    public class FoodBonusData : BonusData
    {
        [SerializeField]
        private int food;

        public int Food => food;
    }



}