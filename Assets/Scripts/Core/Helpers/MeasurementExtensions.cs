using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core.Helpers
{
    /// <summary> 
    /// 
    /// Transform.position  - returns ALWAYS the world position even when it's a nested child
    /// Transform.localPosition - returns the local position relative to it's parent. If an object doesn't have a parent it's the same as Transform.position.
    /// 
    /// Transform.TransformPoint() - transforms a local coordinate inside this Transform into world space.
    /// Transform.InverseTransformPoint() - transforms a world coordinate into the local space of this Transform. 
    /// Transform.TransformDirection() - transforms a local shift vector inside this Transform into world space.
    /// Transform.InverseTransformDirection() - transforms a world shift vector into the local space of this Transform. 
    /// 
    /// </summary>
    public static class MeasurementExtensions
    {
        #region PUBLIC METHODS

        public static Vector3 GetCameraRelativePositionVector(this GameObject obj)
        {
            Camera objectCamera = obj.GetRelevantCameraByHierarchy();
            return obj.gameObject.transform.position - objectCamera.gameObject.transform.position;
        }

        public static Vector3 GetCameraRelativePositionVector(this GameObject obj, Vector3 offset)
        {
            Camera objectCamera = obj.GetRelevantCameraByHierarchy();
            return (obj.gameObject.transform.position + offset) - objectCamera.gameObject.transform.position;
        }

        public static Vector3 GetCameraRelativePositionVector(Camera camera, Vector3 globalPos)
        {
            return globalPos - camera.gameObject.transform.position;
        }
        
        public static void SetCameraRelativePositionVector(this GameObject obj, Vector3 vector)
        {
            Camera objectCamera = obj.GetRelevantCameraByHierarchy();
            Vector3 position = objectCamera.gameObject.transform.position + vector;
            obj.gameObject.transform.position = position;
        }

        public static Vector3 GetGlobalPositionByCameraRelativeVector(this GameObject obj, Vector3 vector)
        {
            Camera objectCamera = obj.GetRelevantCameraByHierarchy();

            //Debug.LogError("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ GetGlobalPositionByCameraRelativeVector - objectCamera = " + objectCamera.name);

            Vector3 position = objectCamera.gameObject.transform.position + vector;
            return position;
        }

        public static Camera GetRelevantCameraByHierarchy(this GameObject go)
        {
            Camera[] allCameras = Camera.allCameras;
            Camera releventCamera = null;

            List<Camera> relevantCameras = new List<Camera>();
            for (int i = 0; i < allCameras.Length; i++)
            {
                if (allCameras[i].cullingMask.IsBitSet(go.layer))
                {
                    releventCamera = allCameras[i];
                    relevantCameras.Add(allCameras[i]);
                }
            }

            for (int i = 0; i < relevantCameras.Count; i++)
            {
                if (go.transform.IsChildOf(relevantCameras[i].transform))
                {
                    releventCamera = relevantCameras[i];
                    break;
                }
            }

            return (null == releventCamera && relevantCameras.Count > 0) ? relevantCameras[0] : releventCamera;
        }

        public static float GetSizeOfPixel(this Camera camera, Transform transform)
        {
            return camera._GetSizeOfPixel(transform.position);
        }

        public static Vector3 GetSizeInPixels(this GameObject go, Camera camera)
        {
            SkinnedMeshRenderer renderer = go.GetComponentInChildren<SkinnedMeshRenderer>();
            Vector3 size = renderer != null ? renderer.bounds.size : go.transform.lossyScale;

            return camera._GetSizeInPixels(size, go.transform.position);
        }

        public static Vector3 GetSizeInLocalPoints(this GameObject go, Vector3 origin)
        {
            Vector3 result = new Vector3(origin.x, origin.y, origin.z);

            List<Transform> parents = new List<Transform>();

            _GetParents(go.transform, ref parents);

            for (int i = 0; i < parents.Count; i++)
            {
                result.x /= parents[i].localScale.x;
                result.y /= parents[i].localScale.y;
                result.z /= parents[i].localScale.z;
            }

            return result;
        }

        public static Vector3 GetGlobalShiftVector(this GameObject go)
        {
            Camera c = go.GetRelevantCameraByHierarchy();
            return (go.transform.position - c.transform.position);
        }

        public static void AddObjectGlobalShiftVector(this GameObject target, GameObject source)
        {
            Vector3 pos = target.transform.position;
            Vector3 shift = source.GetGlobalShiftVector();

            target.transform.position = new Vector3(
                pos.x + shift.x,
                pos.y + shift.y,
                pos.z + shift.z
                );
        }

        public static Vector3 GetSizeInGlobalPoints(this GameObject go, Vector3 origin)
        {
            Vector3 result = new Vector3(origin.x, origin.y, origin.z);

            List<Transform> parents = new List<Transform>();

            _GetParents(go.transform, ref parents);

            for (int i = 0; i < parents.Count; i++)
            {
                result.x *= parents[i].localScale.x;
                result.y *= parents[i].localScale.y;
                result.z *= parents[i].localScale.z;
            }

            return result;
        }

        public static Vector3 GetPositionInGlobalPoints(this GameObject go, Vector3 origin)
        {
            //Vector3 result = new Vector3(origin.x + go.transform.localPosition.x, origin.y + go.transform.localPosition.y, origin.z + go.transform.localPosition.z);

            Vector3 result = new Vector3(origin.x, origin.y, origin.z);

            List<Transform> parents = new List<Transform>();

            _GetParents(go.transform, ref parents);

            for (int i = 0; i < parents.Count; i++)
            {
                result = parents[i].rotation * result;

                result.x *= parents[i].localScale.x;
                result.y *= parents[i].localScale.y;
                result.z *= parents[i].localScale.z;

                result.x += parents[i].localPosition.x;
                result.y += parents[i].localPosition.y;
                result.z += parents[i].localPosition.z;

            }

            return result;
        }


        public static Vector3 GetShiftInGlobalPoints(this GameObject go, Vector3 origin)
        {
            Vector3 result = new Vector3(origin.x, origin.y, origin.z);

            List<Transform> parents = new List<Transform>();

            _GetParents(go.transform, ref parents);

            for (int i = 0; i < parents.Count; i++)
            {
                result.x *= parents[i].localScale.x;
                result.y *= parents[i].localScale.y;
                result.z *= parents[i].localScale.z;
            }
            return result;
        }

        public static Vector3 GetPositionInPixels(this GameObject go, Camera camera)
        {
            return camera._GetPositionInPixels(go.transform.position);
        }

        public static float GetOffsetInLocalSpace(this Camera camera, GameObject go, Vector3 viewport)
        {
            float camOffset = 0.0f;
            Vector2 offset = go.GetOffsetInLocalPoints(camera, viewport);

            camOffset = offset.x > 0 ? offset.x : offset.y;

            return camOffset;
        }

        #endregion

        #region TEMPRUARY PRIVATE

        private static Vector2 GetOffsetInPixels(this GameObject go, Camera camera, Vector3 viewport)
        {
            Vector3 goPosition = camera._GetPositionInPixels(go.transform.position);
            Vector3 goSize = camera._GetSizeInPixels(go.transform.lossyScale, go.transform.position);

            float pixelSize = camera._GetSizeOfPixel(go.transform.position);
            float cameraXPos = camera.transform.position.x / pixelSize;

            Vector2 offset = new Vector2(goPosition.x + goSize.x / 2.0f - viewport.x / 2.0f,
                goPosition.x - goSize.x / 2.0f + viewport.x / 2.0f);

            offset.x -= cameraXPos;
            offset.y -= cameraXPos;

            offset.x = offset.x > 0 ? offset.x : 0.0f; //right
            offset.y = offset.y < 0 ? offset.y : 0.0f; //left

            return offset;
        }

        private static Vector2 GetOffsetInPoints(this GameObject go, Camera camera, Vector3 viewport)
        {
            Vector2 offset = go.GetOffsetInPixels(camera, viewport);

            float pixelSize = camera.GetSizeOfPixel(go.transform);
            offset.x *= pixelSize;
            offset.y *= pixelSize;

            return offset;
        }

        private static Vector2 GetOffsetInLocalPoints(this GameObject go, Camera camera, Vector3 viewport)
        {
            Vector2 offset = go.GetOffsetInPoints(camera, viewport);
            Vector3 localSize = camera.gameObject.GetSizeInLocalPoints(new Vector3(offset.x, offset.y, 0.0f));

            return new Vector2(localSize.x, localSize.y);
        }

        #endregion

        #region PRIVATE METHODS

        private static bool IsBitSet(this int value, int pos)
        {
            return (value & (1 << pos)) != 0;
        }

        private static float _GetSizeOfPixel(this Camera camera, Vector3 globalPosition)
        {
            float pixelSize = 0.0f;
            if (camera.orthographic)
            {
                pixelSize = camera.orthographicSize * 2.0f / Screen.height;
            }
            else
            {
                float distance = globalPosition.z - camera.transform.position.z;
                float angle = camera.fieldOfView / 2.0f;
                float screenHeightInUnits = distance * Mathf.Tan(angle * Mathf.Deg2Rad);
                pixelSize = (screenHeightInUnits * 2.0f) / Screen.height;
            }
            return pixelSize;
        }

        private static Vector3 _GetSizeInPixels(this Camera camera, Vector3 globalSize, Vector3 globalPosition)
        {
            float pixelSize = camera._GetSizeOfPixel(globalPosition);
            return new Vector3(globalSize.x / pixelSize, globalSize.y / pixelSize, globalSize.y / pixelSize);
        }

        private static Vector3 _GetPositionInPixels(this Camera camera, Vector3 globalPosition)
        {
            float pixelSize = camera._GetSizeOfPixel(globalPosition);
            return new Vector3(globalPosition.x / pixelSize, globalPosition.y / pixelSize, 0.0f);
        }

        private static void _GetParents(Transform tr, ref List<Transform> parents)
        {
            if (tr.parent != null)
            {
                parents.Add(tr.parent);
                _GetParents(tr.parent, ref parents);
            }
        }

        #endregion
    }
}