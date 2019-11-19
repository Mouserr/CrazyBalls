using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Core
{

    public static class GameDictionary
    {
        public static List<AllyCharacterData> AllyCharactersDictionary = new List<AllyCharacterData>();
        public static List<EnemyCharacterData>EnemyCharactersDictionary = new List<EnemyCharacterData>();
        public static void Load()
        {
            LoadAllies();
            LoadEnemies();
        }


        private  static void LoadAllies()
        {
            AllyCharactersDictionary = ScriptableObjectsLoader.GetAllInstances<AllyCharacterData>().ToList();
        }

        private static void LoadEnemies()
        {
            EnemyCharactersDictionary = ScriptableObjectsLoader.GetAllInstances<EnemyCharacterData>().ToList();
        }
    }

  
}
