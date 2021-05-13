using UnityEngine;
using sp;

public class Main : MonoBehaviour
{
    void Awake()
    {
        Scenes.InitManager();
        Configs.InitManager();
        Profile.InitManager();
        Events.InitManager();
        Sounds.InitManager();
        Server.InitManager();
    }

    void OnEnable()
    {
        Events.LaunchEvent(Events.Types.SceneLoading, Scenes.ActiveScene);
        Events.LaunchEvent(Events.Types.SceneLoading);
    }

    void OnDisable()
    {
        Events.LaunchEvent(Events.Types.SceneUploading, Scenes.ActiveScene);
        Events.LaunchEvent(Events.Types.SceneUploading);
    }

    void OnGUI()
    {
        CheatsPanel.CheatButton();
    }
}
