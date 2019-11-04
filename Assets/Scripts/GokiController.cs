using UnityEngine;

namespace Assets.Scripts
{
    public class GokiController : MonoBehaviour, IBall
    {
        [SerializeField]
        private int _damage;

        public Stat Health { get; set; }
        public int Damage => _damage;
    }
}