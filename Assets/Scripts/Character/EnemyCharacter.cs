using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class EnemyCharacter: Character
    {
        public EnemyCharacterType CharacterType { get; private set; }

        public EnemyCharacter(EnemyCharacterType characterType, int level)
        {
            CharacterType = characterType;
            Level = level;
            LoadStats();
        }

        private void LoadStats()
        {
            //todo: load from somewhere

            var health = new CharacterStat(CharacterStatType.Health, 10, 10);
            this.RegisterStat(health);

            var passiveDamage = new CharacterStat(CharacterStatType.PassiveDamage, 5, 5);
            this.RegisterStat(passiveDamage);
        }
    }
}
