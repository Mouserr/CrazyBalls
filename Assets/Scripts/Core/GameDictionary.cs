using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.ScriptableObjects;
using UnityEditor;
using UnityEngine;

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
            //foreach (var characterData in charactersData)
            //{
            //    var character = new AllyCharacter(characterData);
            //    AllyCharactersDictionary.Add(character);
            //}
        }

        private static void LoadEnemies()
        {
            EnemyCharactersDictionary = ScriptableObjectsLoader.GetAllInstances<EnemyCharacterData>().ToList();
            //foreach (var characterData in charactersData)
            //{
            //    var character = new EnemyCharacter(characterData);
            //    EnemyCharactersDictionary.Add(character);
            //}
        }
    }

  
}
