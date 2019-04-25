public static class GameStatesManager
{
    private static State currentState;

    public static void EnableState(State state)
    {
        if (currentState == null)
        {
            currentState = state;
            currentState.OnStart(delegate { currentState.OnIdle(null); });
        }
        else
        {
            if (currentState == state) return;
            currentState.OnExit(delegate { state.OnStart(delegate { state.OnIdle(delegate { currentState = state; }); }); });         
        }
    }
}
