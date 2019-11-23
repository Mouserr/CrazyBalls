﻿using System;
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
        private string mapImage;
        [SerializeField]
        private string battlefieldImage;
        [SerializeField]
        private CharacterData[] enemies;
        [SerializeField]
        private BonusData[] bonusesWin;
        [SerializeField]
        private BonusData[] bonusesLoose;

        public string Name => name;
        public string Description => description;
        public CharacterData[] Enemies => enemies;
        public string MapImage => mapImage;
        public string BattlefieldImage => battlefieldImage;
    }
}
