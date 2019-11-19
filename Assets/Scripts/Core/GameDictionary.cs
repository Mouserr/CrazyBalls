using System;
using System.Collections.Generic;
using System.IO;
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
        public static List<ActiveCharacterData> AllyCharactersDictionary = new List<ActiveCharacterData>();
        public static List<CharacterData>EnemyCharactersDictionary = new List<CharacterData>();
        public static void Load()
        {
            LoadAllies();
            LoadEnemies();
        }


        private  static void LoadAllies()
        {
            AllyCharactersDictionary = ScriptableObjectsLoader.GetAllInstances<ActiveCharacterData>().ToList();
            //foreach (var characterData in charactersData)
            //{
            //    var character = new AllyCharacter(characterData);
            //    AllyCharactersDictionary.Add(character);
            //}
        }

        private static void LoadEnemies()
        {
            EnemyCharactersDictionary = ScriptableObjectsLoader.GetAllInstances<CharacterData>().ToList();
            //foreach (var characterData in charactersData)
            //{
            //    var character = new EnemyCharacter(characterData);
            //    EnemyCharactersDictionary.Add(character);
            //}
        }
    }

  
}
