using UnityEngine;
using sp;

public class ButtonBack : MonoBehaviour
{
    public void OnButtonBackClicked()
    {
        Events.LaunchEvent(Events.Types.ExitShop, Scenes.ActiveScene);
    }
}
