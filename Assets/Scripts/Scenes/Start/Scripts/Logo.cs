using UnityEngine;
using sp;

public class Logo : MonoBehaviour
{
    public void ShowMenu()
    {
        Events.LaunchEvent(Events.Types.ScreensaverShown, Scenes.ActiveScene);
    }
}
