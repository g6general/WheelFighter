using UnityEngine;
using sp;

public class GameData
{
    public static int AddedCurrency { get; set; }

    public static void AddCurrency(int value = 1)
    {
        Profile.Data.Currency += value;
    }

    public static bool ReduceCurrency(int value = 1)
    {
        if (!IsEnoughCurrency(value))
            return false;

        Profile.Data.Currency -= value;

        return true;
    }

    public static bool IsEnoughCurrency(int value)
    {
        return (Profile.Data.Currency - value) >= 0;
    }

    public static int GetCurrency()
    {
        return Profile.Data.Currency;
    }

    public static void IncreaseScore()
    {
        ++Profile.Data.Score;
    }

    public static int GetScore()
    {
        return Profile.Data.Score;
    }

    public static void RestartGame()
    {
        Scenes.ResetManager();
        Configs.ResetManager();
        Profile.ResetManager();
        Events.ResetManager();
        Sounds.ResetManager();
        Server.ResetManager();

        LevelPlayground.SetLevelState(LevelPlayground.LevelState.Unknown);
        Scenes.GoToScene(Scenes.Types.Start);

        Debug.Log("Game restarted");
    }
}
