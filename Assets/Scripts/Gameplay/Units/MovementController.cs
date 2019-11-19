using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MovementController : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;

        public float Friction;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            var velocityMagnitude = _rigidbody.velocity.magnitude;
            if (velocityMagnitude <= Friction * Time.deltaTime)
            {
                _rigidbody.velocity = Vector2.zero;
            }
            else
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized * (velocityMagnitude - Friction);
            }
        }
    }
}