using UnityEngine;
using System;
#pragma warning disable
public static class TouchUtils
{
    #region Touch or Mouse Input
    public static bool IsTouchOrMouseDown()
    {
#if !UNITY_EDITOR
        if (Input.touchCount >= 1)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                return true;
            }
        }
        return false;
#else
        return Input.GetMouseButtonDown(0);
#endif
    }
    public static bool IsTouchOrMouseHolding()
    {
#if !UNITY_EDITOR
        if (Input.touchCount >= 1)
        {
            if (Input.touches[0].phase == TouchPhase.Moved)
            {
                return true;
            }
        }
        return false;
#else
        return Input.GetMouseButton(0);
#endif
    }

    public static bool IsTouchOrMouseUp()
    {
#if !UNITY_EDITOR
        if (Input.touchCount >= 1)
        {
            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                return true;
            }
        }
        return false;
#else
        return Input.GetMouseButtonUp(0);
#endif
    }

    public static Vector3 GetTouchOrMousePosition()
    {
#if !UNITY_EDITOR
        if (Input.touchCount >= 1)
        {
            return Input.touches[0].position;
        }
        return Vector3.one;
#else
        return Input.mousePosition;
#endif
    }
    #endregion
    
    public static Vector3 CenterPosition()
    {
        return Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
    }
    
}