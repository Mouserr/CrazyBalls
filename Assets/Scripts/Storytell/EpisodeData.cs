using System;
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
        private Vector2 mapPointPosition;
        [SerializeField]
        private EnemyCharacterData[] enemies;
        [SerializeField]
        private BonusData[] bonusesWin;
        [SerializeField]
        private BonusData[] bonusesLoose;

        public string Name => name;
        public string Description => description;
        public Vector2 MapPointPosition => mapPointPosition;
        public EnemyCharacterData[] Enemies => enemies;
    }
}
