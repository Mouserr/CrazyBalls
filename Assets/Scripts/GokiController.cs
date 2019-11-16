using UnityEngine;

namespace Assets.Scripts
{
    public class GokiController : MonoBehaviour, ICharacter
    {
        [SerializeField]
        private int _damage;

        public Stat Health { get; set; }
        public int Damage => _damage;
    }
}