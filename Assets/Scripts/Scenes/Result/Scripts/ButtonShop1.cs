using UnityEngine;
using sp;

public class ButtonShop1 : MonoBehaviour
{
    public void GoToShop()
    {
        Events.LaunchEvent(Events.Types.LoadShop, Scenes.ActiveScene);
    }
}
