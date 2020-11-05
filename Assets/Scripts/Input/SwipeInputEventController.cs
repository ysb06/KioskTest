using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

namespace KioskTest.Input
{
    /// <summary>
    /// 유니티가 Swipe를 지원하지 않으므로 별도의 처리 코드를 구현
    /// </summary>
    public class SwipeInputEventController : MonoBehaviour
    {
        public const int SENSITIVITY_THRESHOLD = 100;
        private bool pressing = false;
        private Vector2? startPoint = new Vector2();
        private bool IsSwiped = false;

        [Header("Input Event")]
        public InputEvent OnSwipeEvent = new InputEvent();
        public InputEvent OnSwipeEndEvent = new InputEvent();


        public void OnClick(InputAction.CallbackContext context)
        {
            switch(context.phase)
            {
                case InputActionPhase.Started:
                    pressing = true;
                    break;
                case InputActionPhase.Canceled:
                    if (IsSwiped)
                    {
                        OnSwipeEndEvent.Invoke(gameObject, new InputEvent.EventArgs());
                    }
                    IsSwiped = false;
                    pressing = false;
                    startPoint = null;
                    break;
            }
        }

        public void OnPoint(InputAction.CallbackContext context)
        {
            if(pressing && context.phase == InputActionPhase.Performed)
            {
                if(startPoint == null)
                {
                    startPoint = context.ReadValue<Vector2>();
                }
                else
                {
                    Vector2? result = context.ReadValue<Vector2>() - startPoint;
                    if(result != null && (result.Value.x > SENSITIVITY_THRESHOLD || result.Value.x < -SENSITIVITY_THRESHOLD))
                    {
                        IsSwiped = true;
                        OnSwipeEvent.Invoke(gameObject, new InputEvent.EventArgs()
                        {
                            value = result != null ? result.Value.x : 0
                        });
                    }
                }
            }
        }
    }
}