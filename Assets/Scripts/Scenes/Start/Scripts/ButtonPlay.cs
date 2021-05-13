using UnityEngine;
using sp;

public class ButtonPlay : MonoBehaviour
{
    public void GoToLevel()
    {
        Events.LaunchEvent(Events.Types.LoadLevel, Scenes.ActiveScene);
    }
}
