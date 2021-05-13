using UnityEngine;
using sp;

public class Prize : MonoBehaviour
{
    public void StartAnimation()
    {
        transform.SetParent(null);
    }

    public void EndAnimation()
    {
        GameData.AddedCurrency = 1;
        Events.LaunchEvent(Events.Types.IncreaseCurrency);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GetComponent<Animation>().Play();
        GetComponent<AudioSource>().Play();
    }
}
