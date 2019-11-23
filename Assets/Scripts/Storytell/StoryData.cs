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
        private string name;
        [SerializeField]
        private string description;
        [SerializeField]
        private string image;
        [SerializeField]
        private ChapterData[] chapters;

        public string Name => name;
        public string Description => description;
        public string Image => image;
        private IEnumerable<ChapterData> Chapters => chapters;
    }
}