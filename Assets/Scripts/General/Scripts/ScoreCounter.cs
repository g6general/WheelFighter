using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    void Update()
    {
        GetComponent<Text>().text = GameData.GetScore().ToString();
    }
}
