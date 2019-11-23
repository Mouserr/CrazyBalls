using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class UnitAura : MonoBehaviour
    {
        public delegate void CollisionHandler(UnitController auraHolder, UnitController target);

        private CollisionHandler _currentHandler;
        private UnitController _auraHolder;

        public CircleCollider2D AuraCollider;
        public GameObject View;

        public void Attach(UnitController auraHolder, CollisionHandler onCollision)
        {
            _currentHandler = onCollision;
            _auraHolder = auraHolder;
            transform.SetParent(auraHolder.transform);
            transform.localPosition = Vector3.zero;
            _auraHolder.AttachAura(this);
        }
        
        public virtual bool ShouldEnd(UnitController currentUnit)
        {
            return currentUnit == _auraHolder;
        }

        public void SetSize(float radius)
        {
            AuraCollider.radius = radius;
           // View.transform.localScale = radius * Vector2.one;
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            var unitController = other.GetComponentInParent<UnitController>();
            if (unitController == null)
            {
                return;
            }
            
            _currentHandler?.Invoke(_auraHolder, unitController);
        }
    }
}