using UnityEngine;
using sp;

public class Wheel : MonoBehaviour
{
    public float CurrentSpeed = 0;

    private float mSpeedParamA;
    private float mSpeedParamB;
    private float mSpeedParamC;

    private void Start()
    {
        mSpeedParamA = LevelPlayground.WheelParamA;
        mSpeedParamB = LevelPlayground.WheelParamB;
        mSpeedParamC = LevelPlayground.WheelParamC;
    }

    private void Update()
    {
        float speed = lawOfMotion(mSpeedParamA, mSpeedParamB, mSpeedParamC);
        transform.Rotate(0, 0, speed * Time.deltaTime);
        CurrentSpeed = speed;
    }

    private float lawOfMotion(float paramA, float paramB, float paramC)
    {
        return paramA + paramB * Mathf.Cos(Time.time * paramC);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Events.LaunchEvent(Events.Types.IncreaseScore, Scenes.Types.Level);
    }
}
