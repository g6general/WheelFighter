using UnityEngine;
using sp;

public class Bullet : MonoBehaviour
{
    public float Speed;

    private bool mIsFired  = false;
    private bool mIsMotion = false;

    private void FixedUpdate()
    {
        if (mIsMotion)
            transform.Translate(0, Speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (mIsFired)
            return;

        if (other.gameObject.name == "Wheel")
        {
            OnTriggerWheel(other.gameObject);
            Events.LaunchEvent(Events.Types.NextBullet, Scenes.ActiveScene);
        }
        else if (other.gameObject.name == "barrier" || other.gameObject.name == "bullet")
        {
            LevelPlayground.SetLevelState(LevelPlayground.LevelState.LoseLevel);
            LevelPlayground.ActiveBulletIndex = 0;
            OnTriggerBarrier(other.gameObject);
        }
    }

    public void LaunchBullet()
    {
        mIsMotion = true;
        LevelPlayground.ActiveBullet = null;
    }

    private void OnTriggerWheel(GameObject wheel)
    {
        mIsMotion = false;
        mIsFired = true;

        wheel.GetComponent<Animation>().Play();

        transform.SetParent(wheel.GetComponent<Transform>());
        wheel.GetComponent<AudioSource>().Play();
    }

    private void OnTriggerBarrier(GameObject barrier)
    {
        mIsMotion = false;
        mIsFired = true;

        GameObject.Find("Wheel").GetComponent<Animation>().Play();

        barrier.GetComponent<AudioSource>().Play();
        gameObject.GetComponent<Animation>().Play();
    }

    private void OnLose()
    {
        Destroy(gameObject);

        LevelPlayground.SetLevelState(LevelPlayground.LevelState.LoseLevel);

        Scenes.GoToScene(Scenes.Types.Result);
    }
}
