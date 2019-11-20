using System;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MovementController : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        
        [SerializeField]
        private UnitController _unitController;

        public float Friction;
       

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            var velocityMagnitude = _rigidbody.velocity.magnitude;
            if (velocityMagnitude <= Friction)
            {
                _rigidbody.velocity = Vector2.zero;
                _unitController.IsMoving = false;
            }
            else
            {
                _unitController.IsMoving = true;
                _rigidbody.velocity = _rigidbody.velocity.normalized * (velocityMagnitude - Friction * Time.deltaTime);
            }
        }
    }
}