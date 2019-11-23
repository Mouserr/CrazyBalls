using System;
using Assets.Scripts.Models;
using Assets.Scripts.Units;
using UnityEngine;

namespace Assets.Scripts.Configs
{
    [Serializable]
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Character Data")]
    public class CharacterData : ScriptableObject
    {
        [SerializeField]
        private string id;
        [SerializeField]
        private string displayName;
        [SerializeField]
        private string description;
        [SerializeField]
        private Sprite icon;
        [SerializeField]
        private Sprite image;
        [SerializeField]
        private Sprite inGameSprite;
        [SerializeField] 
        private IntAbilityParameter health;
        [SerializeField]
        private IntAbilityParameter energy;
        [SerializeField]
        private IntAbilityParameter passiveDamage;
        [SerializeField]
        private IntAbilityParameter maxSpeed;
        [SerializeField]
        [Tooltip("When ally hits this character")]
        private AbilityConfig passiveAbility;
        [SerializeField]
        [Tooltip("Manually casting")]
        private ActiveAbilityConfig activeAbility;
        [SerializeField]
        private UnitType unitType;

        public CharacterData()
        {
            id = Guid.NewGuid().ToString();
        }

        public string Id => id;
        public string Name => name;
        public string Description => description;
        public Sprite Icon => icon;
        public Sprite Image => image;
        public Sprite InGameSprite => inGameSprite;
        public IntAbilityParameter Health => health;
        public IntAbilityParameter Energy => energy;
        public IntAbilityParameter PassiveDamage => passiveDamage;
        public IntAbilityParameter MaxSpeed => maxSpeed;

        public AbilityConfig PassiveAbility => passiveAbility;
        public ActiveAbilityConfig ActiveAbility => activeAbility;
        public UnitType UnitType => unitType;
    }
}
