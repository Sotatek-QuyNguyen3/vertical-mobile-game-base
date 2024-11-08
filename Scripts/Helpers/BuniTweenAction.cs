using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuniTweenAction : MonoBehaviour
{
    [NonSerialized] public float duration;
    public Vector2 range;
    public AnimationCurve curve;
    bool _ignoreTimeScale = false;
    public float currentTime = 0;
    public Action<float> action;
    public Action onStart, onFinish, onDestroy;
    public float clampDeltaTime = -1;

    void Start()
    {
        if (onStart != null) onStart.Invoke();
        action.Invoke(range.x);
    }

    public static BuniTweenAction AddTweenAction(GameObject gameObject, Action<float> action, Vector2 range, float duration, AnimationCurve curve = null)
    {
        BuniTweenAction tween = gameObject.AddComponent<BuniTweenAction>();
        tween.action = action;
        tween.range = range;
        tween.duration = duration;
        tween.curve = curve;
        return tween;
    }
    public BuniTweenAction IgnoreTimeScale(bool _ignoreTimeScale) { this._ignoreTimeScale = _ignoreTimeScale; return this; }

    void Update()
    {
        float deltaTime = _ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
        if (clampDeltaTime > 0) deltaTime = Mathf.Clamp(deltaTime, 0, clampDeltaTime);
        if (deltaTime == 0) return;
        currentTime += deltaTime;
        if (currentTime >= duration)
        {
            action.Invoke(range.y);
            if (onFinish != null) onFinish.Invoke();
            Destroy(this);
        }
        else
        {
            if (curve != null) action.Invoke(Mathf.LerpUnclamped(range.x, range.y, curve.Evaluate(currentTime / duration)));
            else action.Invoke(Mathf.Lerp(range.x, range.y, currentTime / duration));
        }
    }

    void OnDestroy()
    {
        if (onDestroy != null) onDestroy.Invoke();
    }
}