using System;

public enum GameStatusEnum
{
    NotStarted,
    IsPlaying,
    GameOver,
    Pause
}

class GameStatus
{
    public static GameStatusEnum Current { get; private set; } = GameStatusEnum.NotStarted;

    public static event Action<GameStatusEnum> Changed;

    public static void Change(GameStatusEnum gameStatus)
    {
        Current = gameStatus;
        if(Changed != null)
        {
            Changed(Current);
        }
    }

}