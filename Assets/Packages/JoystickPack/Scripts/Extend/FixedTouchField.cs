using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.UI;

namespace ThanhDV.Joystick
{
    public class FixedTouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [HideInInspector] public Vector2 TouchDist;
        [HideInInspector] public Vector2 PointerOld;
        [HideInInspector] protected int PointerId;
        [HideInInspector] protected int TouchId = -1;
        [HideInInspector] protected bool IsMouse;
        [HideInInspector] public bool Pressed;

        private void Update()
        {
            if (!Pressed)
            {
                TouchDist = Vector2.zero;
                return;
            }

            Vector2 current = GetPointerPosition();
            TouchDist = current - PointerOld;
            PointerOld = current;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Pressed = true;
            PointerId = eventData.pointerId;
            if (eventData is ExtendedPointerEventData ext)
            {
                TouchId = ext.touchId;
                IsMouse = ext.pointerType == UIPointerType.MouseOrPen;
            }
            else
            {
                TouchId = -1;
                IsMouse = false;
            }
            PointerOld = eventData.position;
            TouchDist = Vector2.zero;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Pressed = false;
            PointerId = -1;
            TouchId = -1;
            IsMouse = false;
            TouchDist = Vector2.zero;
        }

        private Vector2 GetPointerPosition()
        {
            if (IsMouse && Mouse.current != null)
                return Mouse.current.position.ReadValue();

            if (Touchscreen.current != null && TouchId > 0)
            {
                foreach (TouchControl touch in Touchscreen.current.touches)
                {
                    if (touch.touchId.ReadValue() == TouchId && touch.press.isPressed)
                        return touch.position.ReadValue();
                }
            }

            return PointerOld;
        }
    }
}