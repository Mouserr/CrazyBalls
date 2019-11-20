using System;
using System.Collections.Generic;
using Assets.Scripts.Configs;
using Assets.Scripts.Units;

namespace Assets.Scripts
{
    public class Character: ICharacter
    {
        public string Id { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public UnitType UnitType { get; protected set; }
        public string Icon { get; protected set; }
        public int Level { get; set; }

        public Dictionary<CharacterStatType, CharacterStat> Stats { get; }
        public List<CharacterAbility> Abilities { get; }

        public CharacterActiveAbility ActiveAbility { get; }
        public CharacterAbility PassiveAbility { get; }
       
        public void RegisterStat(CharacterStat stat)
        {
            if (Stats.ContainsKey(stat.Type)) return;
            Stats.Add(stat.Type, stat);
        }

        public Character(CharacterData characterData)
        {
            Stats = new Dictionary<CharacterStatType, CharacterStat>();
            Abilities = new List<CharacterAbility>();
            Id = characterData.Id;
            Name = characterData.Name;
            Description = characterData.Description;
            Icon = characterData.Icon;

            var health = new CharacterStat(CharacterStatType.Health, characterData.Health, characterData.Health);
            this.RegisterStat(health);

            var energy = new CharacterStat(CharacterStatType.Energy, characterData.Energy, characterData.Energy);
            this.RegisterStat(energy);

            var passiveDamage = new CharacterStat(CharacterStatType.PassiveDamage, characterData.PassiveDamage, characterData.PassiveDamage);
            this.RegisterStat(passiveDamage);

            var maxSpeed = new CharacterStat(CharacterStatType.MaxSpeed, characterData.MaxSpeed, characterData.MaxSpeed);
            this.RegisterStat(maxSpeed);

            ActiveAbility = new CharacterActiveAbility(characterData.ActiveAbility, this);
            if (characterData.PassiveAbility)
            {
                PassiveAbility = new CharacterAbility(characterData.PassiveAbility, this);
            }
            UnitType = characterData.UnitType;
        }
    }
}