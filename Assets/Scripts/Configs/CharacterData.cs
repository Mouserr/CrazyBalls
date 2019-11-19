using System;
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
        private string icon;
        [SerializeField]
        private string image;
        [SerializeField] 
        private int health;
        [SerializeField]
        private int energy;
        [SerializeField]
        private int passiveDamage;
        [SerializeField]
        private int maxSpeed;
        [SerializeField]
        [Tooltip("When ally hits this character")]
        private AbilityConfig passiveAbility;
        [SerializeField]
        [Tooltip("Manually casting")]
        private ActiveAbilityConfig activeAbility;

        public CharacterData()
        {
            id = Guid.NewGuid().ToString();
        }

        public string Id => id;
        public string Name => name;
        public string Description => description;
        public string Icon => icon;
        public string Image => image;
        public int Health => health;
        public int Energy => energy;
        public int PassiveDamage => passiveDamage;
        public int MaxSpeed => maxSpeed;

        public AbilityConfig PassiveAbility => passiveAbility;
        public ActiveAbilityConfig ActiveAbility => activeAbility;
    }
}
