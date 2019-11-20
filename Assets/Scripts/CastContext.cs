using UnityEngine;

namespace Assets.Scripts
{
    public struct CastContext
    {
        public int CasterPlayerId;
        public Vector3 TargetPoint;
        public Transform TargetTransform;
        public Vector3 CasterPoint;
    }
}