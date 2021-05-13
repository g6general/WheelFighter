using UnityEngine;
using UnityEngine.UI;
using sp;

public class ButtonSound : MonoBehaviour
{
    private void Start()
    {
        if (!Sounds.IsSoundOn())
        {
            Color newColor = new Color();
            ColorUtility.TryParseHtmlString("#ABABAB", out newColor);

            var buttonImg = GetComponent<Image>();
            buttonImg.color = newColor;
        }
    }

    public void OnButtonSound()
    {
        var buttonImg = GetComponent<Image>();
        Color newColor = new Color();

        Sounds.Switch();

        if (Sounds.IsSoundOn())
        {
            ColorUtility.TryParseHtmlString("#FFFFFF", out newColor);
            buttonImg.color = newColor;
        }
        else
        {
            ColorUtility.TryParseHtmlString("#ABABAB", out newColor);
            buttonImg.color = newColor;
        }
    }
}
