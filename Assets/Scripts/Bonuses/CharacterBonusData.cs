using System;
using Assets.Scripts.Configs;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    [CreateAssetMenu(fileName = "NewCharacterBonus", menuName = "Bonus: Character")]
    public class CharacterBonusData : BonusData
    {
        [SerializeField]
        private CharacterData allyCharacter;

        public CharacterData AllyCharacter => allyCharacter;
    }
}