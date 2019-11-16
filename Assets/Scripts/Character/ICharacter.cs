using System.Collections.Generic;

namespace Assets.Scripts
{
    public interface ICharacter
    {
        string Name { get; }
        string Description { get; }
        int Level { get; set; }
        Dictionary<CharacterStatType, CharacterStat> Stats { get; }
        Dictionary<CharacterAbilityType, CharacterAbility> Abilities { get; }
        void RegisterStat(CharacterStat stat);
        void RegisterAbility(CharacterAbility ability);
    }
}