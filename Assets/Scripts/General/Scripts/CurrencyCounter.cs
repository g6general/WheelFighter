using UnityEngine;
using UnityEngine.UI;

public class CurrencyCounter : MonoBehaviour
{
    private void Update()
    {
        GetComponent<Text>().text = GameData.GetCurrency().ToString();
    }
}
