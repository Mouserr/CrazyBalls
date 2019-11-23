using System;
using System.Collections.Generic;
using Assets.Scripts.Configs;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    [CreateAssetMenu(fileName = "Player", menuName = "Player: Initial Data Easy")]
    public class PlayerData: ScriptableObject
    {
        [SerializeField]
        private int food;
        [SerializeField]
        private CharacterData[] characters;
        public int Food => food;
        public CharacterData[] Characters => characters;
    }
}