using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Configs;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Core
{

    public static class GameDictionary
    {
        public static List<CharacterData> CharactersDictionary = new List<CharacterData>();
        public static void Load()
        {
            LoadEnemies();
        }


        private static void LoadEnemies()
        {
            CharactersDictionary = ScriptableObjectsLoader.GetAllInstances<CharacterData>().ToList();
            //foreach (var characterData in charactersData)
            //{
            //    var character = new EnemyCharacter(characterData);
            //    CharactersDictionary.Add(character);
            //}
        }
    }

  
}
