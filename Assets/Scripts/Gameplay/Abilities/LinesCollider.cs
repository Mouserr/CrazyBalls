using System;
using UnityEngine;

namespace Assets.Scripts.Abilities
{
    public class LinesCollider : MonoBehaviour
    {
        public event Action<UnitController> OnTrigger;

        public void TriggerEnter2D(Collider2D collision)
        {
            var unit = collision.transform.GetComponentInParent<UnitController>();
            if (unit != null)
            {
                OnTrigger?.Invoke(unit);
            }
        }
    }
}