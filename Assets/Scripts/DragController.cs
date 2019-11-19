using System;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Assets.Scripts
{
    // TODO: вынести отрисовку стрелки для реализации сетевого режима
    public class DragController : MonoBehaviour
    {
        private static DragController _instance;
        private bool _isPressed;
        private Vector2 _mouseInitialPosition;
        private Vector2 _prevPosition;
        private Vector3 _casterPosition;
        private bool _isActive;

        public Camera Camera;
        public Canvas Canvas;
        public Image Arrow;
        
        public event Action<Vector2, float> Swipe; 

        public static DragController Instance
        {
            get { return _instance ?? (_instance = Object.FindObjectOfType<DragController>()); }
        }

        public void Activate(Vector2 position)
        {
            _casterPosition = position;
            _isActive = true;
        }

        void Update()
        {
            if (!_isActive)
            {
                return;
            }

            if (!_isPressed && Input.GetMouseButtonDown(0))
            {
                _isPressed = true;
                _mouseInitialPosition = Input.mousePosition;
                Arrow.enabled = true;
            
                Arrow.transform.position = GetCanvasPosition(_casterPosition);
                Arrow.fillAmount = 0;
                return;
            }

            if (_isPressed)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    _isPressed = false;
                    ProceedDrag();
                    Arrow.enabled = false;
                    return;
                }

                _prevPosition = Input.mousePosition;
                var delta = _mouseInitialPosition - _prevPosition;
                float scaleFactor = Canvas.scaleFactor;

                Arrow.fillAmount = delta.magnitude / scaleFactor / Arrow.GetComponent<RectTransform>().sizeDelta.y;
                Arrow.transform.rotation = Quaternion.FromToRotation(Vector3.up, delta);
            }
        }

        public Vector3 GetCanvasPosition(Vector3 worldSpacePosition)
        {
            var screenPosition = Camera.WorldToScreenPoint(worldSpacePosition);

            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(Canvas.transform as RectTransform, screenPosition, Camera, out position);

            return Canvas.transform.TransformPoint(position);
        }

        private void ProceedDrag()
        {
            var delta = _mouseInitialPosition - _prevPosition;
            _isActive = false;
            Swipe?.Invoke(delta.normalized, Arrow.fillAmount);
        }
    }
}
