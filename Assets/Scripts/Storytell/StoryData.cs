using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    [CreateAssetMenu(fileName = "StoryData", menuName = "Storytell: Story")]
    public class StoryData : ScriptableObject
    {
        [SerializeField]
        private EpisodeData[] episodes;
        public EpisodeData[] Episodes => episodes;
    }
}