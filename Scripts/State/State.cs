using System;

public class State
{
    private state currentState = state.Hide;

    public enum state
    {
        Hide,
        Start,
        Idle,
        Exit
    }

    public void SetState(state newState)
    {
        currentState = newState;
    }

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

