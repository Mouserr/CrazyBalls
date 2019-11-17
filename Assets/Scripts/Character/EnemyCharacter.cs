using Assets.Scripts.ScriptableObjects;

namespace Assets.Scripts
{
    public class EnemyCharacter: Character
    {
        public EnemyCharacter(EnemyCharacterData characterData)
        {
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
        }
    }
}
