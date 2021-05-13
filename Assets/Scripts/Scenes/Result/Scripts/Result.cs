using UnityEngine;
using sp;

public class Result : MonoBehaviour
{
    void OnGUI()
    {
        if (CheatsPanel.NeedShowPanel)
        {
            CheatsPanel.CheatPanel();
        }
    }
}
