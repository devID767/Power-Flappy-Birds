using System;

public enum PointsType { Score, Coins }

public class Score
{
    public static int Record { get; private set; }
    public static bool TrySetRecord(int value)
    {
        if (value < 0)
            return false;

        Record = value;
        return true;
    }
    public static int count { get; private set; }

    public static event Action<int> Changed;

    public static void IncreasePoint()
    {
        count++;
        if (count > Record)
            Record = count;
        Changed?.Invoke(count);
    }

    public static void ClearRecord()
    {
        Record = 0;
    }
    public static void ClearByRound()
    {
        count = 0;
    }
}

public class Coins
{
    public static int EarnByRound { get; private set; }
    public static int count { get; private set; }
    public static bool TrySetCount(int value)
    {
        if (value < 0)
            return false;

        count = value;
        return true;
    }

    public static event Action<int> Increased;
    public static event Action<int> ChangedCount;

    public static void IncreasePoint()
    {
        EarnByRound++;
        count++;
        if (Increased != null)
        {
            Increased(EarnByRound);
        }
    }

    public static bool AddPoints(int value)
    {
        if (count + value < 0)
        {
            return false;
        }

        count += value;
        ChangedCount?.Invoke(count);
        SaveManager.instance.SaveGame();

        return true;
    }

    public static void ClearAllCoins()
    {
        count = 0;
    }
    public static void ClearCoinsByRound()
    {
        EarnByRound = 0;
    }
}
