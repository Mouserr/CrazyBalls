using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [Serializable]
    [CreateAssetMenu(fileName = "NewEnemyCharacter", menuName = "Enemy Character")]
    public class EnemyCharacterData : ScriptableObject
    {
        [SerializeField]
        private string id;
        [SerializeField]
        private string name;
        [SerializeField]
        private string description;
        [SerializeField]
        private string icon;
        [SerializeField]
        private string image;
        [SerializeField] 
        private int health;
        [SerializeField]
        private int energy;
        [SerializeField]
        private int passiveDamage;

        public EnemyCharacterData()
        {
            id = Guid.NewGuid().ToString();
        }

        public string Id => id;
        public string Name => name;
        public string Description => description;
        public string Icon => icon;
        public string Image => image;
        public int Health => health;
        public int Energy => energy;
        public int PassiveDamage => passiveDamage;
    }
}
