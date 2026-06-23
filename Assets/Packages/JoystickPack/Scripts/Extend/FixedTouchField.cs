using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace ThanhDV.Joystick
{
    public class FixedTouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [HideInInspector] public Vector2 TouchDist;
        [HideInInspector] public Vector2 PointerOld;
        [HideInInspector] protected int PointerId;
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
            PointerOld = eventData.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Pressed = false;
        }

        private Vector2 GetPointerPosition()
        {
            // Negative pointerId means mouse (left=-1, right=-2, middle=-3). Non-negative is a touch index.
            if (PointerId >= 0 && Touchscreen.current != null)
            {
                var touches = Touchscreen.current.touches;
                if (PointerId < touches.Count)
                {
                    TouchControl touch = touches[PointerId];
                    if (touch.press.isPressed)
                        return touch.position.ReadValue();
                }
            }

            if (Mouse.current != null)
                return Mouse.current.position.ReadValue();

            if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
                return Touchscreen.current.primaryTouch.position.ReadValue();

            return PointerOld;
        }
    }
}
