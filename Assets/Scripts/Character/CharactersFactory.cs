using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class EnemiesCharacterFactory
    {
        public static Character Create(EnemyCharacterType characterType, int level)
        {
           return new EnemyCharacter(characterType, level);
        }
    }

    public class AlliesCharacterFactory
    {
        public static Character Create(AllyCharacterType characterType, int level)
        {
            return new AllyCharacter(characterType, level);
        }
    }
}
