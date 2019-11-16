using System.Collections.Generic;

namespace Assets.Scripts
{
    public abstract class Character: ICharacter
    {
        public string Name { get; }
        public string Description { get; }
        public int Level { get; set; }
        public Dictionary<CharacterStatType, CharacterStat> Stats { get; }
        public Dictionary<CharacterAbilityType, CharacterAbility> Abilities { get; }
        public void RegisterStat(CharacterStat stat)
        {
            if (Stats.ContainsKey(stat.Type)) return;
            Stats.Add(stat.Type, stat);
        }

        public void RegisterAbility(CharacterAbility ability)
        {
            if (Abilities.ContainsKey(ability.Type)) return;
            Abilities.Add(ability.Type, ability);
        }

        public Character()
        {
            Stats = new Dictionary<CharacterStatType, CharacterStat>();
            Abilities = new Dictionary<CharacterAbilityType, CharacterAbility>();
        }
    }
}