using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void OnButtonClick()
    {
        var panel = GameObject.Find("CheatsCanvas");
        panel.SetActive(false);
    }
}
