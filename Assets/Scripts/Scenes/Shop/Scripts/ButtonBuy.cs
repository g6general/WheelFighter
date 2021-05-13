using UnityEngine;
using sp;

public class ButtonBuy : MonoBehaviour
{
    public void OnButtonBuyClicked()
    {
        Events.LaunchEvent(Events.Types.Purchase, Scenes.ActiveScene);
    }
}
