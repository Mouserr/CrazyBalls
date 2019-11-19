using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    [CreateAssetMenu(fileName = "NewChapter", menuName = "Storytell: Chapter")]
    public class ChapterData : ScriptableObject
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
        private EpisodeData[] episodes;

        public string Name => name;
        public string Description => description;
        public string MapImage => mapImage;
        public string BattlefieldImage => battlefieldImage;
        private IEnumerable<EpisodeData> Episodes => episodes;
    }
}
