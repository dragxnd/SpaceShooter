using System;
using UnityEngine;

public class UiPanel : MonoBehaviour
{
    public enum state
    {
        Hide,
        Start,
        Idle,
        Exit
    }

    public state State { get; set; }

    public virtual void OnStart(Action callback)
    {
        callback?.Invoke();
    }

    public virtual void OnIdle(Action callback)
    {
        callback?.Invoke();
    }

    public virtual void OnExit(Action callback)
    {
        callback?.Invoke();
    }

}
