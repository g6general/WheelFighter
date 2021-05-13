using UnityEngine;
using sp;

public class StartService : MonoBehaviour
{
    void OnGUI()
    {
        if (CheatsPanel.NeedShowPanel)
        {
            CheatsPanel.CheatPanel();
        }
    }
}
