using System;
using Assets.Scripts;
using Assets.Scripts.Screens;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Assets.Scripts
{
    public class UIDragController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
         private static UIDragController _instance;
        private bool _isPressed;
        private Vector2 _mouseInitialPosition;
        private Vector2 _prevPosition;
        private Vector3 _casterPosition;
        private bool _isActive;

        public float MinDelta;
        public Camera Camera;
        public Canvas Canvas;
        public Image Arrow;
        public Graphic Graphic;
        
        public event Action<Vector2, float> Swipe; 

        public static UIDragController Instance
        {
            get { return _instance ?? (_instance = Object.FindObjectOfType<UIDragController>()); }
        }

        public void Activate(Vector2 position)
        {
            _casterPosition = position;
            Graphic.raycastTarget = true;
            _isActive = true;
        }

        public void Deactivate()
        {
            _isActive = false;
            Graphic.raycastTarget = false;
            _isPressed = false;
        }

        void Update()
        {
            if (!_isActive)
            {
                return;
            }

            if (_isPressed)
            {
                float scaleFactor = Canvas.scaleFactor;
                _prevPosition = Input.mousePosition;
                var delta = _mouseInitialPosition - _prevPosition;
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
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_isActive)
            {
                return;
            }

            if (!_isPressed)
            {
                _isPressed = true;
                _mouseInitialPosition = Input.mousePosition;
                Arrow.enabled = true;
            
                Arrow.transform.position = GetCanvasPosition(_casterPosition);
                Arrow.fillAmount = 0;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_isActive || !_isPressed)
            {
                return;
            }

            _prevPosition = Input.mousePosition;
            var delta = _mouseInitialPosition - _prevPosition;
            _isPressed = false;
            if (delta.sqrMagnitude > MinDelta)
            {
                ProceedDrag();
                Arrow.enabled = false;
            }
        }
    }
}