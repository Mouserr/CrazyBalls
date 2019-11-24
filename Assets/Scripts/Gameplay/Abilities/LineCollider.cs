using UnityEngine;

namespace Assets.Scripts.Abilities
{
    public class LineCollider : MonoBehaviour
    {
        [SerializeField]
        private LinesCollider LinesCollider;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            LinesCollider.TriggerEnter2D(collision);
        }
    }
}