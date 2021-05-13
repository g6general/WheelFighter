using UnityEngine;
using sp;

public class ButtonShop : MonoBehaviour
{
    public void GoToShop()
    {
        Events.LaunchEvent(Events.Types.LoadShop, Scenes.ActiveScene);
    }
}
