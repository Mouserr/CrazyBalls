using System;
using Assets.Scripts.Configs;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    [CreateAssetMenu(fileName = "NewEpisode", menuName = "Storytell: Episode")]
    public class EpisodeData : ScriptableObject
    {
        [SerializeField]
        private string name;
        [SerializeField]
        private string description;
        [SerializeField]
        private Sprite battlefieldImage;
        [SerializeField]
        private CharacterData[] enemies;
        [SerializeField]
        private BonusData[] bonusesWin;
        [SerializeField]
        private BonusData[] bonusesLoose;
        [SerializeField]
        private int[] enemyLevels;

        public string Name => name;
        public string Description => description;
        public CharacterData[] Enemies => enemies;
        public int[] EnemyLevels => enemyLevels;
       
        public BonusData[] BonusesWin => bonusesWin;
        public BonusData[] BonusesLoose => bonusesLoose;
        public Sprite BattlefieldImage => battlefieldImage;
    }
}
