using System.Collections.Generic;

namespace Assets.Scripts
{
    public interface ICharacter
    {
        string Name { get; }
        string Description { get; }
        Dictionary<CharacterStatType, CharacterStat> Stats { get; }
        List<CharacterAbility> Abilities { get; }
        void RegisterStat(CharacterStat stat);
    }
}