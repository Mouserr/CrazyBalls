using System;
using Assets.Scripts.Configs;
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
        private CharacterData allyCharacter;

        public CharacterData AllyCharacter => allyCharacter;
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