using System.Collections.Generic;

namespace Assets.Scripts
{
    public interface ICharacter
    {
        string Name { get; }
        string Description { get; }
        int Level { get; }
        Dictionary<CharacterStatType, CharacterStat> Stats { get; }
        void RegisterStat(CharacterStat stat);
        void AddEffect(CharacterEffect effectConfig);
        void SetLevel(int level);
    }
}