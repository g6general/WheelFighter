using UnityEngine;
using sp;

public class ButtonBg : MonoBehaviour
{
    public void OnButtonClick()
    {
        Events.LaunchEvent(Events.Types.GameTap, Scenes.ActiveScene);
    }
}
