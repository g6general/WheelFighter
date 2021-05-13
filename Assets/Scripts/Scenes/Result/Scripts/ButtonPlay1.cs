using UnityEngine;
using sp;

public class ButtonPlay1 : MonoBehaviour
{
    public void GoToLevel()
    {
        if (LevelPlayground.GetLevelState() == LevelPlayground.LevelState.LoseLevel)
        {
            if (GameData.IsEnoughCurrency(Configs.Balance.PaymentForContinue))
                Events.LaunchEvent(Events.Types.ContinueLevelByMoney, Scenes.ActiveScene);
            else
                Events.LaunchEvent(Events.Types.NotEnoughMoneyToContinue, Scenes.ActiveScene);
        }
        else
        {
            Events.LaunchEvent(Events.Types.ContinueLevel, Scenes.ActiveScene);
        }
    }
}
