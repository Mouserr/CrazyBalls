using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{

    public class AllyCharacter : Character
    {
        public AllyCharacter(AllyCharacterData characterData)
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
