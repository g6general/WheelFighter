using UnityEngine;
using sp;

public class ButtonDeny : MonoBehaviour
{
    public void GoToStart()
    {
        Events.LaunchEvent(Events.Types.LoadStart, Scenes.ActiveScene);
    }
}
