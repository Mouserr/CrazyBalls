using UnityEngine;

namespace Assets.Scripts
{
    public struct CastContext
    {
        public Vector3 TargetPoint;
        public Transform TargetTransform;

        public UnitController Caster;
        public UnitController Target;
    }
}